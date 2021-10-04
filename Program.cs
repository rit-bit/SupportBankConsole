using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Transactions;
using NLog;
using NLog.Config;
using NLog.Targets;
using SupportBankConsole.Exporters;
using SupportBankConsole.Importers;

namespace SupportBankConsole
{
    class Program : ICommands
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            InitialiseLogging();
            var p = new Program();
            while (true)
            {
                try
                {
                    UserInput.GetUserInput(p);
                }
                catch (ArgumentException exception)
                {
                    Logger.Error(exception);
                    Console.WriteLine("Program could not load transactions due to the following error.");
                    Console.WriteLine(exception.Message);
                }
            }
        }
    

        private static void InitialiseLogging()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
        }
        
        public void ListAll()
        {
            Logger.Info("User selected \"List All\" command");
            foreach (var person in Person.GetPeople())
            {
                Console.WriteLine(person);
            }
        }

        public void ListAccount(string name)
        {
            Logger.Info("User selected \"List [Filename]\" command for file \"{name}\"");

            foreach (var transaction in Transaction.GetTransactions())
            {
                if (transaction.FromAccount.Name == name)
                {
                    Console.WriteLine(transaction);
                }
            }
        }
        public void TryToImportFile(string fileName)
        {
            Logger.Info("User selected \"Import File\" command for file \"{fileName}\"");
            try
            {
                var importer = IImporter.GetImporter(fileName);
                importer.Import(fileName);
            }
            catch (NotSupportedException exception)
            {
                Console.WriteLine(exception.Message);
                Logger.Error(exception);
            }
        }

        public void TryToExportFile(string fileName)
        {
            Logger.Info("User selected \"Export File\" command for file \"{fileName}\"");
            try
            {
                var exporter = IExporter.GetExporter(fileName);
                exporter.Export(fileName);
            }
            catch (NotSupportedException exception)
            {
                Console.WriteLine(exception.Message);
                Logger.Error(exception);
            }
        }
        
    }
}