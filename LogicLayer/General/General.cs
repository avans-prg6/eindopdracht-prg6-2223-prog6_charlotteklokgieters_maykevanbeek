namespace LogicLayer.General
{
    public class General
    {
        public string[] SplitNames(string names)
        {
            string[] kids;

            if (names.Contains(", "))
            {
                kids = names.Split(", ");
            }
            else if (names.Contains(","))
            {
                kids = names.Split(",");
            }
            else
            {
                kids = new string[1];
                kids[0] = names;
            }

            return kids;
        }
    }
}
