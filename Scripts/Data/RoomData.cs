using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : Singleton<RoomData>
{
    #region General
    public string roomId;
    public PlayerID playerId;
    #endregion

    #region OpponentInformation
    public string otherUserId;
    public string otherUsername;
    public int otherScore;
    #endregion

    #region Gameplay
    [SerializeField]
    private PlayerID _turn;
    public PlayerID Turn
    {
        get
        {
            return _turn;
        }
        set
        {
            _turn = value;
        }
    }
    #endregion

    #region Result
    [SerializeField]
    private PlayerID _result;
    public PlayerID Result
    {
        get
        {
            return _result;
        }
        set
        {
            _result = value;
        }
    }
    #endregion

    #region Others
    public List<Room> roomList = new List<Room>();

    [SerializeField]
    private bool _playerAReady;
    public bool PlayerAReady
    {
        get
        {
            return _playerAReady;
        }
        set
        {
            if(value == true && UserData.Instance.gameState == GameState.Transaction)
            {
                FindObjectOfType<Transaction>().playerAReadyText.text = "Player A Oyuna Hazır!";
                FindObjectOfType<Transaction>().playerAReadyText.color = new Color32(65, 203, 41, 255);
            }
            _playerAReady = value;
        }
    }

    [SerializeField]
    private bool _playerBReady;
    public bool PlayerBReady
    {
        get
        {
            return _playerBReady;
        }
        set
        {
            if (value == true && UserData.Instance.gameState == GameState.Transaction)
            {
                FindObjectOfType<Transaction>().playerBReadyText.text = "Player B Oyuna Hazır!";
                FindObjectOfType<Transaction>().playerBReadyText.color = new Color32(65, 203, 41, 255);
            }
            _playerBReady = value;
        }
    }
    #endregion
}
