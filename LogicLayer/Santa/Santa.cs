using System.ComponentModel.DataAnnotations;

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

        public static ValidationResult CheckDubbleNames(string kidsnames, ValidationContext context)
        {
            string[] kids = kidsnames.Split(", ");
            bool error = false;
            List<string> dubbles = new List<string>();
            List<string> accounts = new List<string>();
            foreach (string kid in kids)
            {
                if (!accounts.Contains(kid))
                {
                    accounts.Add(kid);
                }
                else
                {
                    error = true;
                    dubbles.Add(kid);
                }
            }

            if (error)
            {
                string message = "De volgende namen komen al voor: ";
                foreach (string name in dubbles)
                {
                    message += name + " ";
                }

                return new ValidationResult(
                    string.Format(message, context.MemberName),
                    new List<string>() { context.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
