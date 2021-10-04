using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NLog;

namespace SupportBankConsole.Importers
{
    public class JsonImporter : IImporter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        IEnumerable<Transaction> IImporter.ImportFromFile(string path)
        {
            using var reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            Logger.Info("Attempting to deserialize ");
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