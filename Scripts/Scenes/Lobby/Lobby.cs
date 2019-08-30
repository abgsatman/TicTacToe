using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    private DBManager DB;
    private AuthManager auth;

    public Dropdown gameListForm;
    public Button joinRoom;

    public Button createRoom;

    private UserData user;
    private RoomData room;

    void Start()
    {
        DB = DBManager.Instance;
        auth = AuthManager.Instance;
        user = UserData.Instance;

        user.gameState = GameState.Lobby;

        joinRoom.onClick.AddListener(JoinRoom);
        createRoom.onClick.AddListener(CreateRoom);

        DB.GetRoomList(gameListForm);
    }

    void JoinRoom()
    {
        Debug.Log(gameListForm.options[gameListForm.value].text);
        string _roomId = gameListForm.options[gameListForm.value].text;
        DB.SendInvite(_roomId);
    }

    void CreateRoom()
    {
        DB.CreateRoom();
    }
}
