using System;
using System.Globalization;
using NLog;

namespace SupportBankConsole
{
    public class Conversions
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static DateTime ConvertStringToDate(string DateString)
        {
            if (!Validation.IsDateValidCsv(DateString))
            {
                if (!Validation.IsDateValidJson(DateString))
                {
                    var errorMessage = $"Invalid date input {DateString} could not be processed as a date.";
                    Logger.Fatal(errorMessage);
                    throw new ArgumentException(errorMessage);
                }
                return DateTime.ParseExact(DateString, "yyyy/M/d", CultureInfo.InvariantCulture);
            }
            return DateTime.ParseExact(DateString, "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        
        public static decimal ConvertStringToDecimal(string DecimalString)
        {
            if (!Validation.IsDecimalValid(DecimalString))
            {
                var errorMessage = $"Invalid decimal input {DecimalString} could not be processed as an amount.";
                Logger.Fatal(errorMessage);
                throw new ArgumentException(errorMessage);
            }
            return Convert.ToDecimal(DecimalString);
        }
    }
}