using System.Text.RegularExpressions;

namespace SupportBankConsole
{
    public class Validation
    {
        public static bool IsDecimalValid(string decimalString)
        {
            Regex rx = new Regex(@"^(\d+)?(\.\d{1,2})?$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(decimalString);
            return matches.Count > 0;
        }
    }
}