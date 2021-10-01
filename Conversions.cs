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
            if (DateString.Contains(" "))
            {
                var dateParts = DateString.Split(" ");
                DateString = dateParts[0];
            }
            if (Validation.IsDateValid(DateString))
            {
                return DateTime.ParseExact(DateString, "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            
            var errorMessage = $"Invalid date input {DateString} could not be processed as a date.";
            Logger.Fatal(errorMessage);
            throw new ArgumentException(errorMessage);
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

        public static DateTime ConvertXMLToDate(string DateString)
        {
            var dateNumber = Convert.ToInt32(DateString);
            var date = new DateTime(1900,01,01);
            date = date.AddDays(dateNumber);
            return date;
        }
        
    }
}