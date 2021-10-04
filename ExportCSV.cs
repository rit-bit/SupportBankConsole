using System;

namespace SupportBankConsole
{
    public class ExportCsv
    {
        public static void Export(string fileName)
        {
            var stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("Date,From,To,Narrative,Amount");
            foreach (var item in Transaction.GetTransactions())
            {
                stringBuilder.AppendLine(ConvertTransactionToCsvString(item));
            }
            
            System.IO.File.WriteAllText(@"C:\Work\Outputs\" + fileName,
                stringBuilder.ToString());
            Console.WriteLine($"File {fileName} has been successfully created");
        }

        private static string ConvertTransactionToCsvString(Transaction t)
        {
            return ($"{t.Date:d},{t.From.Name},{t.To.Name},{t.Narrative},{t.Amount}");
        }
    }
}