/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of TicTacToe project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData : Singleton<BoardData>
{
    public string lastPlayed;

    [SerializeField]
    private string _s1;
    public string S1
    {
        get
        {
            return _s1;
        }
        set
        {
            if(value != _s1 && UserData.Instance.gameState == GameState.Gameplay)
            {
                if(value == "X")
                {
                    Instantiate(GameObject.Find("s1").GetComponent<GameController>().X, GameObject.Find("s1").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    lastPlayed = "X";
                }
                else if(value == "O")
                {
                    Instantiate(GameObject.Find("s1").GetComponent<GameController>().O, GameObject.Find("s1").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    lastPlayed = "O";
                }
                GameObject.Find("s1").GetComponent<GameController>().status = true;
            }
            _s1 = value;
        }
    }

    [SerializeField]
    private string _s2;
    public string S2
    {
        get
        {
            return _s2;
        }
        set
        {
            if (value != _s2 && UserData.Instance.gameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s2").GetComponent<GameController>().X, GameObject.Find("s2").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    lastPlayed = "X";
                }
                else if (value == "O")
                {
                    Instantiate(GameObject.Find("s2").GetComponent<GameController>().O, GameObject.Find("s2").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    lastPlayed = "O";
                }
                GameObject.Find("s2").GetComponent<GameController>().status = true;
            }
            _s2 = value;
        }
    }

    [SerializeField]
    private string _s3;
    public string S3
    {
        get
        {
            return _s3;
        }
        set
        {
            if (value != _s3 && UserData.Instance.gameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s3").GetComponent<GameController>().X, GameObject.Find("s3").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    lastPlayed = "X";
                }
                else if (value == "O")
                {
                    Instantiate(GameObject.Find("s3").GetComponent<GameController>().O, GameObject.Find("s3").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    lastPlayed = "O";
                }
                GameObject.Find("s3").GetComponent<GameController>().status = true;
            }
            _s3 = value;
        }
    }

    [SerializeField]
    private string _s4;
    public string S4
    {
        get
        {
            return _s4;
        }
        set
        {
            if (value != _s4 && UserData.Instance.gameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s4").GetComponent<GameController>().X, GameObject.Find("s4").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    lastPlayed = "X";
                }
                else if (value == "O")
                {
                    Instantiate(GameObject.Find("s4").GetComponent<GameController>().O, GameObject.Find("s4").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    lastPlayed = "O";
                }
                GameObject.Find("s4").GetComponent<GameController>().status = true;
            }
            _s4 = value;
        }
    }

    [SerializeField]
    private string _s5;
    public string S5
    {
        get
        {
            return _s5;
        }
        set
        {
            if (value != _s5 && UserData.Instance.gameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s5").GetComponent<GameController>().X, GameObject.Find("s5").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    lastPlayed = "X";
                }
                else if (value == "O")
                {
                    Instantiate(GameObject.Find("s5").GetComponent<GameController>().O, GameObject.Find("s5").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    lastPlayed = "O";
                }
                GameObject.Find("s5").GetComponent<GameController>().status = true;
            }
            _s5 = value;
        }
    }

    [SerializeField]
    private string _s6;
    public string S6
    {
        get
        {
            return _s6;
        }
        set
        {
            if (value != _s6 && UserData.Instance.gameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s6").GetComponent<GameController>().X, GameObject.Find("s6").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    lastPlayed = "X";
                }
                else if (value == "O")
                {
                    Instantiate(GameObject.Find("s6").GetComponent<GameController>().O, GameObject.Find("s6").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    lastPlayed = "O";
                }
                GameObject.Find("s6").GetComponent<GameController>().status = true;
            }
            _s6 = value;
        }
    }

    [SerializeField]
    private string _s7;
    public string S7
    {
        get
        {
            return _s7;
        }
        set
        {
            if (value != _s7 && UserData.Instance.gameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s7").GetComponent<GameController>().X, GameObject.Find("s7").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    lastPlayed = "X";
                }
                else if (value == "O")
                {
                    Instantiate(GameObject.Find("s7").GetComponent<GameController>().O, GameObject.Find("s7").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    lastPlayed = "O";
                }
                GameObject.Find("s7").GetComponent<GameController>().status = true;
            }
            _s7 = value;
        }
    }

    [SerializeField]
    private string _s8;
    public string S8
    {
        get
        {
            return _s8;
        }
        set
        {
            if (value != _s8 && UserData.Instance.gameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s8").GetComponent<GameController>().X, GameObject.Find("s8").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    lastPlayed = "X";
                }
                else if (value == "O")
                {
                    Instantiate(GameObject.Find("s8").GetComponent<GameController>().O, GameObject.Find("s8").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    lastPlayed = "O";
                }
                GameObject.Find("s8").GetComponent<GameController>().status = true;
            }
            _s8 = value;
        }
    }

    [SerializeField]
    private string _s9;
    public string S9
    {
        get
        {
            return _s9;
        }
        set
        {
            if (value != _s9 && UserData.Instance.gameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s9").GetComponent<GameController>().X, GameObject.Find("s9").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    lastPlayed = "X";
                }
                else if (value == "O")
                {
                    Instantiate(GameObject.Find("s9").GetComponent<GameController>().O, GameObject.Find("s9").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    lastPlayed = "O";
                }
                GameObject.Find("s9").GetComponent<GameController>().status = true;
            }
            _s9 = value;
        }
    }
}
