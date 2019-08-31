using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public Text NoticeText;

    private UserData user;
    private DBManager DB;

    void Start()
    {
        user = UserData.Instance;
        DB = DBManager.Instance;

        user.gameState = GameState.Gameplay;

        //DB.CloseListenAcceptedInvites();
        DB.CloseListenInvites();

        NoticeText.text = RoomData.Instance.roomId;
    }
}
