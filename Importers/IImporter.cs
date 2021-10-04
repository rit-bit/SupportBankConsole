using System;
using System.Collections.Generic;
using NLog;

namespace SupportBankConsole.Importers
{
    public interface IImporter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static IImporter GetImporter(string path)
        {
            var index = path.LastIndexOf(".", StringComparison.Ordinal);
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
                Logger.Info($"Adjusting account balances for the following transaction: {transaction}");
                transaction.FromAccount.DecreaseAmount(transaction.Amount);
                transaction.ToAccount.IncreaseAmount(transaction.Amount);
            }
            var msg = $"File {path} was imported successfully.";
            Console.WriteLine(msg);
            Logger.Info(msg);
        }
    }
}