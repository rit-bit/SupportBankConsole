using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Transactions;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBankConsole
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            InitialiseLogging();
            var p = new Program();
            ImportCSV.ImportCsv("./Transactions2014.csv");
            ImportCSV.ImportCsv("./DodgyTransactions2015.csv");
            while (true)
            {
                p.UserInput();
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

        private void UserInput()
        {
            Console.WriteLine("Please enter \"List All\" or \"List [Account]\" where [Account] is a name");
            Logger.Info("Requested the user to input a command");
            var input = Console.ReadLine();
            Logger.Info($"User inputted {input}");
            var firstWord = input.Substring(0, 5);
            if (firstWord != "List ")
            {
                Logger.Warn("User did not enter a valid command starting with \"List \"");
                return;
            }
            var secondWord = input[5..];
            switch (secondWord)
            {
                case ("All"):
                    ListAll();
                    break;
                default:
                    ListAccount(secondWord);
                    break;
            }

        }

        private void ListAll()
        {
            foreach (var person in Person.GetPeople())
            {
                Console.WriteLine(person);
            }
        }

        private void ListAccount(string name)
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