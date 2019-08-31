/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of TicTacToe project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    #region Logic
    public bool isLogin = false;
    public GameState gameState;
    #endregion

    #region General
    [SerializeField]
    private string _userId;
    public string UserID
    {
        get
        {
            return _userId;
        }
        set
        {
            _userId = value;
        }
    }

    [SerializeField]
    private string _username;
    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
        }
    }
    #endregion

    #region Progression
    [SerializeField]
    private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }
    #endregion
}
