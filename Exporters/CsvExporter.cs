namespace SupportBankConsole.Exporters
{
    public class CsvExporter : IExporter
    {
        void IExporter.ExportToFile(string filename)
        {
            var stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("Date,From,To,Narrative,Amount");
            foreach (var item in Transaction.GetTransactions())
            {
                stringBuilder.AppendLine(ConvertTransactionToCsvString(item));
            }

            System.IO.File.WriteAllText(@"C:\Work\Outputs\" + filename,
                stringBuilder.ToString());
        }

        private static string ConvertTransactionToCsvString(Transaction t)
        {
            return ($"{t.Date:d},{t.FromAccount.Name},{t.ToAccount.Name},{t.Narrative},{t.Amount}");
        }
    }
}