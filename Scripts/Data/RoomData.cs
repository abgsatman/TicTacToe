using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : Singleton<RoomData>
{
    #region General
    public string roomId;
    public string playerId;
    #endregion

    #region OpponentInformation
    [SerializeField]
    private string _otherUserId;
    public string OtherUserId
    {
        get
        {
            return _otherUserId;
        }
        set
        {
            if(value != _otherUserId)
            {
                _otherUserId = value;
            }
        }
    }

    public string otherUsername;
    public int otherScore;
    #endregion

    #region Gameplay
    public string turn = "PlayerA";
    #endregion

    #region Result
    [SerializeField]
    private string _result;
    public string Result
    {
        get
        {
            return _result;
        }
        set
        {
            if(value != _result && UserData.Instance.gameState == GameState.Gameplay)
            {
                _result = value;
            }
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
            if (value != _playerAReady && value == true && UserData.Instance.gameState == GameState.Transaction)
            {
                FindObjectOfType<Transaction>().playerAReadyText.text = "Player A Hazır!";
                FindObjectOfType<Transaction>().playerAReadyText.color = new Color32(65, 203, 41, 255);
                _playerAReady = value;
            }
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
            if (value != _playerBReady && value == true && UserData.Instance.gameState == GameState.Transaction)
            {
                FindObjectOfType<Transaction>().playerBReadyText.text = "Player B Hazır!";
                FindObjectOfType<Transaction>().playerBReadyText.color = new Color32(65, 203, 41, 255);
                _playerBReady = value;
            }
        }
    }
    #endregion
}
