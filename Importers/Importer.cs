using System;
using System.Collections.Generic;

namespace SupportBankConsole.Importers
{
    public interface Importer
    {
        public static Importer GetImporter(string path)
        {
            int index = path.LastIndexOf(".", StringComparison.Ordinal);
            switch (path[index..])
            {
                case ".csv":
                    return new CsvImporter();
                case ".json":
                    return new JsonImporter();
                case ".xml":
                    return new XmlImporter();
                default:
                    throw new NotSupportedException();
            }
        }

        protected IEnumerable<Transaction> ImportFromFile(string path);

        public void Import(string path)
        {
            foreach (var transaction in ImportFromFile(path))
            {
                transaction.From.DecreaseAmount(transaction.Amount);
                transaction.To.IncreaseAmount(transaction.Amount);
            }
            Console.WriteLine($"File {path} was imported successfully.");
        }
    }
}