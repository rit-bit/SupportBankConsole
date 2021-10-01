using System;
using System.Text.RegularExpressions;
using NLog;

namespace SupportBankConsole
{
    public class Validation
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static bool IsDecimalValid(string decimalString)
        {
            Regex rx = new Regex(@"^(\d+)?(\.\d{1,2})?$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(decimalString);
            return matches.Count > 0;
        }
        
        public static bool IsDateValid(string dateString)
        {
            var rx = new Regex(@"^((?:0?[1-9]|[12][0-9]|3[01])[- /.](?:0?[1-9]|1[012])[- /.](?:19|20)\d\d?)$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = rx.Matches(dateString);
            var toReturn = matches.Count > 0;
            Logger.Debug($"Does date match CSV pattern? {toReturn}");
            return toReturn;
        }
    }
}