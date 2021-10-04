using System.Collections.Generic;
using Newtonsoft.Json;

namespace SupportBankConsole.Exporters
{
    public class JsonExporter : IExporter
    {
        void IExporter.ExportToFile(string filename)
        {
            var listToOutput = new List<TransactionToJson>();
            foreach (var transaction in Transaction.GetTransactions())
            {
                listToOutput.Add(new TransactionToJson(transaction));
            }
            var output = JsonConvert.SerializeObject(listToOutput);
            System.IO.File.WriteAllText(@"C:\Work\Outputs\" + filename,
                output);
        }
    }
}