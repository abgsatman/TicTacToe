using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DBManager : Singleton<DBManager>
{
    public AuthManager auth;

    public UserData user;
    public RoomData room;
    public BoardData board;

    public DatabaseReference usersDatabase;
    public DatabaseReference roomsDatabase;
    public DatabaseReference invitesDatabase;
    public DatabaseReference acceptedInvitesDatabase;

    public string FirebaseDBURL = "https://udemy-deneme-projesi.firebaseio.com/";

    void Start()
    {
        auth = AuthManager.Instance;

        user = UserData.Instance;
        room = RoomData.Instance;
        board = BoardData.Instance;

        Initialization();
    }

    void Initialization()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(FirebaseDBURL);

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                usersDatabase = FirebaseDatabase.DefaultInstance.GetReference("Users");
                roomsDatabase = FirebaseDatabase.DefaultInstance.GetReference("Rooms");
                invitesDatabase = FirebaseDatabase.DefaultInstance.GetReference("Invites");
                acceptedInvitesDatabase = FirebaseDatabase.DefaultInstance.GetReference("AcceptedInvites");

                if(auth.auth.CurrentUser != null)
                {
                    auth.AutoLogin();
                }
                else
                {
                    SceneManager.LoadScene("Login");
                }
            }
            else
            {
                Debug.LogError(String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    public void CreateUser(string username)
    {
        Dictionary<string, object> general = new Dictionary<string, object>();
        general["Username"] = username;

        Dictionary<string, object> progression = new Dictionary<string, object>();
        progression["Score"] = 0;

        usersDatabase.Child(user.UserID).Child("General").UpdateChildrenAsync(general);
        user.Username = username;

        usersDatabase.Child(user.UserID).Child("Progression").UpdateChildrenAsync(progression);
        user.Score = 0;

        Debug.Log("Kullanıcı başarıyla oluşturuldu, login sahnesine yönlendiriliyorsunuz...");

        SceneManager.LoadScene("Login");
    }

    public void GetUserInformation()
    {
        usersDatabase.Child(user.UserID).GetValueAsync().ContinueWith(task =>
        {
            if(task.IsFaulted)
            {
                Debug.Log("faulted");
                return;
            }
            if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                string username = snapshot.Child("General").Child("Username").Value.ToString();
                int score = int.Parse(snapshot.Child("Progression").Child("Score").Value.ToString());

                user.Username = username;
                user.Score = score;

                Debug.Log("Kullanıcı login oldu ve bilgileri çekildi, lobby sahnesine yönlendiriliyorsunuz...");

                SceneManager.LoadScene("Lobby");
            }
        });
    }

    public void CreateRoom()
    {
        string roomId = roomsDatabase.Push().Key;
        room.roomId = roomId;

        Dictionary<string, object> roomDetails = new Dictionary<string, object>();
        roomDetails["PlayerA"] = user.UserID;
        roomDetails["PlayerB"] = "none";
        roomDetails["Result"] = "none";
        roomDetails["PlayerAReady"] = false;
        roomDetails["PlayerBReady"] = false;
        roomDetails["Turn"] = "PlayerA";

        roomsDatabase.Child(roomId).UpdateChildrenAsync(roomDetails);

        room.playerId = PlayerID.PlayerA;

        OpenListenRoom();
        OpenListenInvites();

        Debug.Log("Oda Kuruldu... Diğer oyuncu bekleniyor... Transaction sahnesine yönlendiriliyorsunuz...");
        SceneManager.LoadScene("Transaction");
    }

    public void GetRoomList(Dropdown roomList)
    {
        roomsDatabase.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("faulted");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                List<string> menuList = new List<string>();

                int index = 0;
                foreach (DataSnapshot r in snapshot.Children)
                {
                    string _roomId = r.Key;
                    string _hostId = snapshot.Child(_roomId).Child("PlayerA").Value.ToString();
                    room.roomList.Add(new Room(_roomId, _hostId));

                    menuList.Add(_roomId);
                    index++;
                }

                roomList.AddOptions(menuList);
            }
        });
    }

    public void SendInvite(string roomId)
    {
        Dictionary<string, object> invite = new Dictionary<string, object>();
        invite[user.UserID] = user.UserID;

        invitesDatabase.Child(roomId).UpdateChildrenAsync(invite);

        OpenListenAcceptedInvites();
    }

    public void AcceptInvite(string inviteUserId)
    {
        if(room.playerId == PlayerID.PlayerA)
        {
            invitesDatabase.Child(user.UserID).UpdateChildrenAsync(null);

            Dictionary<string, object> roomDetails = new Dictionary<string, object>();
            roomDetails["PlayerB"] = inviteUserId;
            roomDetails["PlayerBReady"] = true;

            roomsDatabase.Child(room.roomId).UpdateChildrenAsync(roomDetails);

            room.otherUserId = inviteUserId;

            RemoveAllInvites();

            Dictionary<string, object> acceptedInvite = new Dictionary<string, object>();
            acceptedInvite["RoomID"] = room.roomId;

            acceptedInvitesDatabase.Child(inviteUserId).UpdateChildrenAsync(acceptedInvite);

            Debug.Log("Davet kabul edildi... Oyun sahnesine yönlendiriliyorsunuz...");
        }
    }

    public void JoinRoom(string roomId)
    {
        Dictionary<string, object> playerB = new Dictionary<string, object>();
        playerB["PlayerB"] = user.UserID;

        roomsDatabase.Child(room.roomId).UpdateChildrenAsync(playerB);
    }

    public void SetResult()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["Result"] = Extensions.PlayerIDToStringConverter();

        roomsDatabase.Child(room.roomId).UpdateChildrenAsync(result);
    }

    public void SetReady()
    {
        Dictionary<string, object> ready = new Dictionary<string, object>();
        if(room.playerId == PlayerID.PlayerA)
        {
            ready["PlayerAReady"] = true;
        }
        if (room.playerId == PlayerID.PlayerB)
        {
            ready["PlayerBReady"] = true;
        }

        roomsDatabase.Child(room.roomId).UpdateChildrenAsync(ready);
    }

    public void EditTurn()
    {
        Dictionary<string, object> turn = new Dictionary<string, object>();
        turn["Turn"] = Extensions.PlayerIDToStringConverter();

        roomsDatabase.Child(room.roomId).UpdateChildrenAsync(turn);
    }

    public void DoAction(Positions p)
    {
        Dictionary<string, object> action = new Dictionary<string, object>();
        action[p.ToString()] = Extensions.SymbolConverter();

        roomsDatabase.Child(room.roomId).Child("Board").UpdateChildrenAsync(action);
    }

    public void RemoveAllInvites()
    {
        invitesDatabase.Child(room.roomId).UpdateChildrenAsync(null);
    }

    public void OpenListenRoom()
    {
        string _roomId = room.roomId;
        Debug.Log(_roomId);

        FirebaseDatabase.DefaultInstance.GetReference("Rooms").Child(_roomId).ValueChanged += ListenRoom;
    }

    public void ListenRoom(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        DataSnapshot snapshot = args.Snapshot;

        Debug.Log("Değişiklik algılandı #1");

        room.Turn = Extensions.StringToPlayerIDConverter(snapshot.Child("Turn").Value.ToString());

        room.Result = Extensions.StringToPlayerIDConverter(snapshot.Child("Result").Value.ToString());

        room.PlayerAReady = (bool)snapshot.Child("PlayerAReady").GetValue(true);

        room.PlayerBReady = (bool)snapshot.Child("PlayerBReady").GetValue(true);
    }

    public void OpenListenInvites()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Invites").Child(room.roomId).ValueChanged += ListenInvites;
    }

    public void ListenInvites(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        Debug.Log("Değişiklik algılandı #2");

        foreach (DataSnapshot invite in args.Snapshot.Children)
        {
            string inviteUserId = invite.Key;

            if(user.gameState == GameState.Transaction)
            {
                GameObject invitedObject = Instantiate(FindObjectOfType<Transaction>().inviteObject, GameObject.Find("Canvas").transform);
                invitedObject.GetComponent<InviteManager>().otherUserId = inviteUserId;
            }
        }
    }

    public void OpenListenAcceptedInvites()
    {
        FirebaseDatabase.DefaultInstance.GetReference("AcceptedInvites").Child(user.UserID).ValueChanged += ListenAcceptedInvites;
    }

    public void ListenAcceptedInvites(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        DataSnapshot snapshot = args.Snapshot;

        Debug.Log("Değişiklik algılandı #3");

        room.roomId = snapshot.Child("RoomID").Value.ToString();
        room.playerId = PlayerID.PlayerB;

        if(room.roomId != "")
        {
            OpenListenRoom();
            SetReady();

            Debug.Log("eşleşme sağlandı... transaction sahnesine yönlendiriliyorsunuz...");
            SceneManager.LoadScene("Transaction");
        }
    }
}
