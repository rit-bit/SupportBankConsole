using System.IO;
using Newtonsoft.Json;
using NLog;

namespace SupportBankConsole
{
    public class ImportJSON
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static void ImportJson(string path)
        {
            using StreamReader reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (var item in array)
            {
                var date = Conversions.ConvertStringToDate(item.Date.ToString());
                var amount = Conversions.ConvertStringToDecimal(item.Amount);
                new Transaction(date, item.FromAccount, item.ToAccount, item.Narrative, amount);

            }
        }
    }
}