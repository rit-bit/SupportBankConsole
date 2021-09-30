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
        
        public static bool IsDateValid(string dateString)
        {
            Regex rx = new Regex(@"^(0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](19|20)\d\d$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(dateString);
            return matches.Count > 0;
        }
    }
}