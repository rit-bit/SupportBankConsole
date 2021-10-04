using System;

namespace SupportBankConsole
{
    public class ExportCsv
    {
        public static void Export(string fileName)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("Date,From,To,Narrative,Amount");
            foreach (var item in Transaction.GetTransactions())
            {
                stringBuilder.AppendLine(item.ToCsv());
            }
            
            System.IO.File.WriteAllText(@"C:\Work\Outputs\" + fileName,
                stringBuilder.ToString());
            Console.WriteLine($"File {fileName} has been successfully created");
        }
    }
}