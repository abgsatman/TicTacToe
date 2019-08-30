public static class Extensions
{
    public static string SymbolConverter()
    {
        if(RoomData.Instance.playerId == PlayerID.PlayerA)
        {
            return "X";
        }
        else if(RoomData.Instance.playerId == PlayerID.PlayerB)
        {
            return "O";
        }
        else
        {
            return null;
        }
    }

    public static string PlayerIDToStringConverter()
    {
        if (RoomData.Instance.playerId == PlayerID.PlayerA)
        {
            return "PlayerA";
        }
        else if (RoomData.Instance.playerId == PlayerID.PlayerB)
        {
            return "PlayerB";
        }
        else
        {
            return null;
        }
    }

    public static PlayerID StringToPlayerIDConverter(string player)
    {
        if(player == "PlayerA")
        {
            return PlayerID.PlayerA;
        }
        else
        {
            return PlayerID.PlayerB;
        }
    }
}
