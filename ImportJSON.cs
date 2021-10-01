using System;
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
                var from = Person.GetOrCreatePerson(item.FromAccount.ToString());
                var to = Person.GetOrCreatePerson(item.ToAccount.ToString());
                Console.WriteLine($"{date} {from.Name} {to.Name} {item.Narrative.ToString()} {item.Amount}");
                new Transaction(date, from, to, item.Narrative.ToString(), (decimal) item.Amount);

            }
        }
    }
}