namespace LogicLayer.General
{
    public class General
    {
        public static string[] SplitString(string longstring)
        {
            string[] list;

            if (longstring.Contains(", "))
            {
                list = longstring.Split(", ");
            }
            else if (longstring.Contains(","))
            {
                list = longstring.Split(",");
            }
            else
            {
                list = new string[1];
                list[0] = longstring;
            }

            return list;
        }
    }
}
