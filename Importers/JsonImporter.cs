using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NLog;

namespace SupportBankConsole.Importers
{
    public class JsonImporter : Importer
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        IEnumerable<Transaction> Importer.ImportFromFile(string path)
        {
            using StreamReader reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (var item in array)
            {
                var date = Conversions.ConvertStringToDate(item.Date.ToString());
                var from = Person.GetOrCreatePerson(item.FromAccount.ToString());
                var to = Person.GetOrCreatePerson(item.ToAccount.ToString());
                yield return new Transaction(date, from, to, item.Narrative.ToString(), (decimal) item.Amount);

            }
        }
    }
}