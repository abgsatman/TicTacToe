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
    private DBManager DB;

    public Text playerAUsername;
    public Text playerBUsername;

    public Text playerAScore;
    public Text playerBScore;

    public Image playerATurn;
    public Image playerBTurn;

    void Start()
    {
        user = UserData.Instance;
        room = RoomData.Instance;
        DB = DBManager.Instance;

        user.gameState = GameState.Gameplay;

        DB.GetOtherUserInformation();

        DB.CloseListenInvites();

        NoticeText.text = "Siz: " + user.username;

        if (room.playerId == "PlayerA")
            DB.RemoveAllInvites();

        if (room.playerId == "PlayerB")
            DB.RemoveAllAcceptedInvites();
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

    public void ExitButton()
    {
        Debug.Log("Oyundan ayrılma süreci başladı!");
 
        //result bilgisi gönder
        DB.SetResult();
    }
}
