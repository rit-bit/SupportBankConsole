using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NLog;

namespace SupportBankConsole
{
    public class ImportCSV
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        
        public static List<Transaction> ImportCsv(string path)
        {
            var aFile = new FileStream(path, FileMode.Open);
            var streamReader = new StreamReader(aFile);

            var transactions = new List<Transaction>();

            // read data in line by line
            var line = streamReader.ReadLine(); // Remove header from CSV
            while ((line = streamReader.ReadLine()) != null)
            {
                // Console.WriteLine(line);
                var parts = line.Split(",");
                // Console.WriteLine(parts[0]);
                Logger.Info($"Attempting to parse date {parts[0]}");
                var date = DateTime.ParseExact(parts[0], "d/M/yyyy", CultureInfo.InvariantCulture);
                Logger.Info($"Attempting to get person {parts[1]}");
                var from = Person.GetOrCreatePerson(parts[1]);
                Logger.Info($"Attempting to get person {parts[2]}");
                var to = Person.GetOrCreatePerson(parts[2]);
                Logger.Info($"Attempting to get narrative {parts[3]}");
                var narrative = parts[3];
                Logger.Info($"Attempting to get amount {parts[4]}");
                var amount = Convert.ToDecimal(parts[4]);
                var transaction = new Transaction(date, from, to, narrative, amount);
                transactions.Add(transaction);
                from.DecreaseAmount(amount);
                to.IncreaseAmount(amount);
            }
            streamReader.Close();
            return transactions;
        }
    }
}