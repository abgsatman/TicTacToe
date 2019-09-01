/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of TicTacToe project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public Text NoticeText;

    private UserData user;
    private RoomData room;
    private BoardData board;
    private DBManager DB;

    public Text playerAUsername;
    public Text playerBUsername;

    public Text playerAScore;
    public Text playerBScore;

    public Image playerATurn;
    public Image playerBTurn;

    public GameObject g;
    public GameObject X1;
    public GameObject O1;

    void Start()
    {
        user = UserData.Instance;
        room = RoomData.Instance;
        board = BoardData.Instance;
        DB = DBManager.Instance;

        user.gameState = GameState.Gameplay;

        DB.GetOtherUserInformation();

        NoticeText.text = "Siz: " + user.username;

        if (room.playerId == "PlayerA")
        {
            DB.CloseListenInvites();
            DB.RemoveAllInvites();
        }  

        if (room.playerId == "PlayerB")
        {
            DB.CloseListenAcceptedInvites();
            DB.RemoveAllAcceptedInvites();
        }
            
    }

    private void Update()
    {
        if(room.playerId == "PlayerA")
        {
            playerAUsername.text = user.username;
            playerAScore.text = user.score.ToString();
        }
        if (room.playerId == "PlayerB")
        {
            playerBUsername.text = user.username;
            playerBScore.text = user.score.ToString();
        }
    }

    public void CheckTourConditions()
    {
        if (board.lastPlayed == "X")
            g = X1;
        else if (board.lastPlayed == "O")
            g = O1;

        if (board.S1 == board.S2 && board.S2 == board.S3 && board.S3 != "") // 1 2 3
        {
            Instantiate(g, GameObject.Find("s1").transform);
            Instantiate(g, GameObject.Find("s2").transform);
            Instantiate(g, GameObject.Find("s3").transform);

            if(room.playerId == Extensions.ReturnSymbol(board.S1))
                DB.SetResult(true);
        }
        else if (board.S4 == board.S5 && board.S5 == board.S6 && board.S6 != "") // 4 5 6
        {
            Instantiate(g, GameObject.Find("s4").transform);
            Instantiate(g, GameObject.Find("s5").transform);
            Instantiate(g, GameObject.Find("s6").transform);

            if (room.playerId == Extensions.ReturnSymbol(board.S4))
                DB.SetResult(true);
        }
        else if (board.S7 == board.S8 && board.S8 == board.S9 && board.S9 != "") // 7 8 9
        {
            Instantiate(g, GameObject.Find("s7").transform);
            Instantiate(g, GameObject.Find("s8").transform);
            Instantiate(g, GameObject.Find("s9").transform);

            if (room.playerId == Extensions.ReturnSymbol(board.S7))
                DB.SetResult(true);
        }
        else if (board.S1 == board.S5 && board.S5 == board.S9 && board.S9 != "") // 1 5 9
        {
            Instantiate(g, GameObject.Find("s1").transform);
            Instantiate(g, GameObject.Find("s5").transform);
            Instantiate(g, GameObject.Find("s9").transform);

            if (room.playerId == Extensions.ReturnSymbol(board.S1))
                DB.SetResult(true);
        }
        else if (board.S3 == board.S5 && board.S5 == board.S7 && board.S7 != "") // 3 5 7
        {
            Instantiate(g, GameObject.Find("s3").transform);
            Instantiate(g, GameObject.Find("s5").transform);
            Instantiate(g, GameObject.Find("s7").transform);

            if (room.playerId == Extensions.ReturnSymbol(board.S3))
                DB.SetResult(true);
        }
        else if (board.S1 == board.S4 && board.S4 == board.S7 && board.S7 != "") // 1 4 7
        {
            Instantiate(g, GameObject.Find("s1").transform);
            Instantiate(g, GameObject.Find("s4").transform);
            Instantiate(g, GameObject.Find("s7").transform);

            if (room.playerId == Extensions.ReturnSymbol(board.S1))
                DB.SetResult(true);
        }
        else if (board.S2 == board.S5 && board.S5 == board.S8 && board.S8 != "") // 2 5 8
        {
            Instantiate(g, GameObject.Find("s2").transform);
            Instantiate(g, GameObject.Find("s5").transform);
            Instantiate(g, GameObject.Find("s8").transform);

            if (room.playerId == Extensions.ReturnSymbol(board.S2))
                DB.SetResult(true);
        }
        else if (board.S3 == board.S6 && board.S6 == board.S9 && board.S9 != "") // 3 6 9
        {
            Instantiate(g, GameObject.Find("s3").transform);
            Instantiate(g, GameObject.Find("s6").transform);
            Instantiate(g, GameObject.Find("s9").transform);

            if (room.playerId == Extensions.ReturnSymbol(board.S3))
                DB.SetResult(true);
        }
        else
        {
            //
            Debug.Log("herhangi bir eşleşme yok, oyuna devam!");
        }
    }

    public void ExitButton()
    {
        Debug.Log("Oyundan ayrılma süreci başladı!");
 
        //result bilgisi gönder
        DB.SetResult();
    }
}
