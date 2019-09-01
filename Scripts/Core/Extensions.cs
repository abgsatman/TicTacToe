public static class Extensions 
{
    public static string ReturnSymbol(string s)
    {
        if(s == "X")
        {
            return "PlayerA";
        }
        else
        {
            return "PlayerB";
        }
    }
}
