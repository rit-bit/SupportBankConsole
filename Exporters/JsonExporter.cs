using Newtonsoft.Json;

namespace SupportBankConsole.Exporters
{
    public class JsonExporter : IExporter
    {
        void IExporter.ExportToFile(string filename)
        {
            
            var output = JsonConvert.SerializeObject(Transaction.GetTransactions());
            System.IO.File.WriteAllText(@"C:\Work\Outputs\" + filename,
                output);
        }
    }
}