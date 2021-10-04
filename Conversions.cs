using System;
using System.Globalization;
using NLog;

namespace SupportBankConsole
{
    public static class Conversions
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static DateTime ConvertStringToDate(string dateString)
        {
            if (dateString.Contains(" "))
            {
                var dateParts = dateString.Split(" ");
                dateString = dateParts[0];
            }
            if (Validation.IsDateValid(dateString))
            {
                return DateTime.ParseExact(dateString, "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            
            var errorMessage = $"Invalid date input {dateString} could not be processed as a date.";
            Logger.Fatal(errorMessage);
            throw new ArgumentException(errorMessage);
        }
        
        public static decimal ConvertStringToDecimal(string decimalString)
        {
            if (!Validation.IsDecimalValid(decimalString))
            {
                var errorMessage = $"Invalid decimal input {decimalString} could not be processed as an amount.";
                Logger.Fatal(errorMessage);
                throw new ArgumentException(errorMessage);
            }
            return Convert.ToDecimal(decimalString);
        }

    }
}