using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DBManager : Singleton<DBManager>
{
    public AuthManager auth;

    public UserData user;
    public RoomData room;
    public BoardData board;

    public DatabaseReference usersDatabase;
    public DatabaseReference roomsDatabase;
    public DatabaseReference invitesDatabase;
    
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
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                usersDatabase = FirebaseDatabase.DefaultInstance.GetReference("Users");
                roomsDatabase = FirebaseDatabase.DefaultInstance.GetReference("Rooms");
                invitesDatabase = FirebaseDatabase.DefaultInstance.GetReference("Invites");

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
                Debug.LogError(System.String.Format(
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
        roomDetails["PlayerB"] = "";
        roomDetails["ResultA"] = "";
        roomDetails["ResultB"] = "";
        roomDetails["PlayerAReady"] = false;
        roomDetails["PlayerBReady"] = false;

        roomsDatabase.Child(roomId).UpdateChildrenAsync(roomDetails);
    }

    public void GetRoomList()
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

                int index = 0;
                foreach (DataSnapshot r in snapshot.Children)
                {
                    string _roomId = r.Key;
                    string _hostId = snapshot.Child(_roomId).Child("PlayerA").Value.ToString();
                    room.roomList[index] = new Room(_roomId, _hostId);
                    index++;
                }
            }
        });
    }

    public void SendInvite(string hostId)
    {
        Dictionary<string, object> invite = new Dictionary<string, object>();
        invite["InviteUserID"] = user.UserID;

        invitesDatabase.Child(hostId).UpdateChildrenAsync(invite);
    }

    public void AcceptInvide(string inviteUserId)
    {
        invitesDatabase.Child(user.UserID).UpdateChildrenAsync(null);

        Dictionary<string, object> roomDetails = new Dictionary<string, object>();
        roomDetails["PlayerB"] = inviteUserId;

        roomsDatabase.Child(room.roomId).UpdateChildrenAsync(roomDetails);
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
        result["Result"] = Extensions.ResultConverter();

        roomsDatabase.Child(room.roomId).UpdateChildrenAsync(result);
    }

    public void EditTurn()
    {
        Dictionary<string, object> turn = new Dictionary<string, object>();
        turn["Turn"] = Extensions.SymbolConverter();

        roomsDatabase.Child(room.roomId).UpdateChildrenAsync(turn);
    }

    public void DoAction(Positions p)
    {
        Dictionary<string, object> action = new Dictionary<string, object>();
        action[p.ToString()] = Extensions.SymbolConverter();

        roomsDatabase.Child(room.roomId).Child("Board").UpdateChildrenAsync(action);
    }

    public void OpenListenRoom(string roomId)
    {
        if(roomId != "")
        {
            FirebaseDatabase.DefaultInstance.GetReference("Rooms").Child(roomId).ValueChanged += ListenRoom;
        }
    }

    public void ListenRoom(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        if (args.Snapshot.Value.ToString() != "")
        {
            room.Turn = Extensions.StringToPlayerIDConverter(args.Snapshot.Child("Turn").Value.ToString());
            user.Result = Extensions.StringToPlayerIDConverter(args.Snapshot.Child("Result").ToString());
        }
    }

    public void OpenListenInvites()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Invites").Child(user.UserID).ValueChanged += ListenInvites;
    }

    public void ListenInvites(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        foreach (DataSnapshot invite in args.Snapshot.Children)
        {
            string inviteUserId = invite.Key;
            Debug.Log(inviteUserId);
        }
    }
}
