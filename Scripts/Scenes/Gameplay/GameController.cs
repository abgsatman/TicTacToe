using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject X;
    public GameObject O;

    private DBManager DB;

    private UserData user;
    private RoomData room;

    public bool status = false;

    private void Start()
    {
        DB = DBManager.Instance;
        user = UserData.Instance;
        room = RoomData.Instance;
    }

    public void DoAction()
    {
        if (room.playerId == "PlayerA" && room.turn == "PlayerA" && !status)
        {
            DB.DoAction(this.gameObject.name);
        }
        else if (room.playerId == "PlayerB" && room.turn == "PlayerB" && !status)
        {
            DB.DoAction(this.gameObject.name);
        }
    }
}