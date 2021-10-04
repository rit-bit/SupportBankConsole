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
            foreach (var person in Person.GetPeople())
            {
                Console.WriteLine(person);
            }
        }

        public void ListAccount(string name)
        {
            foreach (var transaction in Transaction.GetTransactions())
            {
                if (transaction.FromAccount.Name == name)
                {
                    Console.WriteLine(transaction);
                }
            }
        }
        
        
    }
}