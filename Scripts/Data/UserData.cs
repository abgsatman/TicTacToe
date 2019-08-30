using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    #region Logic
    public bool isLogin = false;
    #endregion

    #region General
    [SerializeField]
    private string userId;
    public string UserID
    {
        get
        {
            return userId;
        }
        set
        {
            userId = value;
        }
    }

    [SerializeField]
    private string username;
    public string Username
    {
        get
        {
            return username;
        }
        set
        {
            username = value;
        }
    }
    #endregion

    #region Progression
    [SerializeField]
    private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    #endregion

    #region Result
    public PlayerID Result;
    #endregion
}
