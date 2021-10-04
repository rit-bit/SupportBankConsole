using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NLog;

namespace SupportBankConsole.Importers
{
    public class CsvImporter : IImporter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        
        IEnumerable<Transaction> IImporter.ImportFromFile(string path)
        {
            var aFile = new FileStream(path, FileMode.Open);
            var streamReader = new StreamReader(aFile);
            
            // read data in line by line
            var line = streamReader.ReadLine(); // Remove header from CSV
            var lineNumber = 1;
            while ((line = streamReader.ReadLine()) != null)
            {
                lineNumber++;
                // Console.WriteLine(line);
                var parts = line.Split(",");
                // Console.WriteLine(parts[0]);
                Logger.Info($"Attempting to parse date {parts[0]}");
                if (!Validation.IsDateValid(parts[0]))
                {
                    var errorMessage = $"Invalid date input \"{parts[0]}\" on line number {lineNumber} could not be processed as a date.";
                    Logger.Fatal(errorMessage);
                    throw new ArgumentException(errorMessage);
                }
                var date = DateTime.ParseExact(parts[0], "d/M/yyyy", CultureInfo.InvariantCulture);
                Logger.Info($"Attempting to get person {parts[1]}");
                var from = Person.GetOrCreatePerson(parts[1]);
                Logger.Info($"Attempting to get person {parts[2]}");
                var to = Person.GetOrCreatePerson(parts[2]);
                Logger.Info($"Attempting to get narrative {parts[3]}");
                var narrative = parts[3];
                Logger.Info($"Attempting to get amount {parts[4]}");
                if (!Validation.IsDecimalValid(parts[4]))
                {
                    var errorMessage = $"Invalid decimal input \"{parts[4]}\" on line number {lineNumber}  could not be processed as an amount.";
                    Logger.Fatal(errorMessage);
                    throw new ArgumentException(errorMessage);
                }
                var amount = Convert.ToDecimal(parts[4]);
                yield return new Transaction(date, from, to, narrative, amount);
            }
            streamReader.Close();
        }
    }
}