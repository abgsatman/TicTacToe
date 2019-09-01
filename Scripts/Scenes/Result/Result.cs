/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of TicTacToe project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    private UserData user;
    private RoomData room;

    public Text result;

    private void Start()
    {
        user = UserData.Instance;
        room = RoomData.Instance;

        if(room.Result == "PlayerA")
        {
            if(room.playerId == "PlayerA")
            {
                result.text = "SEN KAZANDIN!";
            }
            else if(room.playerId == "PlayerB")
            {
                result.text = "KAYBETTİN!";
            }
        }
        else if(room.Result == "PlayerB")
        {
            if (room.playerId == "PlayerA")
            {
                result.text = "KAYBETTİN!";
            }
            else if (room.playerId == "PlayerB")
            {
                result.text = "SEN KAZANDIN!";
            }
        }
    }
    public void GoLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
