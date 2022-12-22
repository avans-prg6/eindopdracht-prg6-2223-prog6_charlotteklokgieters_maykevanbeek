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

        public static List<string> AddErrorDubbles(string message, List<string> dubbles)
        {
            List<string> errors = new List<string>();

            foreach (string name in dubbles)
            {
                message += name + " ";
            }
            errors.Add(message);

            return errors;
        }
    }
}
