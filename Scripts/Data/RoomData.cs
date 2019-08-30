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
    private PlayerID turn;
    public PlayerID Turn
    {
        get
        {
            return turn;
        }
        set
        {
            turn = value;
        }
    }
    #endregion

    #region Others
    public Room[] roomList;
    #endregion
}
