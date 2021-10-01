using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Transactions;
using NLog;
using NLog.Config;
using NLog.Targets;
using SupportBankConsole.Importers;

namespace SupportBankConsole
{
    class Program 
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            InitialiseLogging();
            var p = new Program();
            try
            {
                //ImportCSV.ImportCsv("./Transactions2014.csv");
                //ImportCSV.ImportCsv("./DodgyTransactions2015.csv");
                //ImportJSON.ImportJson("./Transactions2013.json");

                foreach (var transaction in Transaction.GetTransactions())
                {
                    transaction.From.DecreaseAmount(transaction.Amount);
                    transaction.To.IncreaseAmount(transaction.Amount);
                }

                while (true)
                {
                    p.UserInput();
                }
            }
            // TODO - BEN - Catch "file going wrong" exceptions differently to "user input going wrong"
            catch (ArgumentException exception)
            {
                Console.WriteLine("Program could not load transactions due to the following error.");
                Console.WriteLine(exception.Message);
                Console.WriteLine("Program needs to exit.");
                Environment.Exit(-1);
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
            foreach (var person in Person.GetPeople())
            {
                Console.WriteLine(person);
            }
        }

        public void ListAccount(string name)
        {
            foreach (var transaction in Transaction.GetTransactions())
            {
                if (transaction.From.Name == name)
                {
                    Console.WriteLine(transaction);
                }
            }
        }
        
        
    }
}