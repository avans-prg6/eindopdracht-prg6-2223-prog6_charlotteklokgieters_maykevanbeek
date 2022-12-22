namespace LogicLayer.Santa
{
    public class Santa
    {
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
