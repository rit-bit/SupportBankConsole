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

        // TODO - BEN - Maybe you could make a "UserInput" class
        private void UserInput()
        {
            Console.WriteLine("\nThese are the available commands: ");
            Console.WriteLine("- List All");
            Console.WriteLine("- List [Account] where [Account] is a name");
            Console.WriteLine("- Import File [Filename] where [Filename] is the files name");
            Console.WriteLine("Please enter one of the commands: ");
            Logger.Info("Requested the user to input a command");
            var input = Console.ReadLine();
            var inputParts = input.Split(" ");
            Logger.Info($"User inputted {input}");
            if (inputParts.Length == 0)
            {
                return;
            }

            if (inputParts[0] == "List")
            {
                //TODO - BEN - Index out of range
                if(inputParts[1] == "All")
                {
                    ListAll();
                }
                else
                {
                    var name = input[5..];
                    ListAccount(name);
                }
            }

            // TODO  BEN - Index out of range
            if (inputParts[0] == "Import" && inputParts[1] == "File")
            {
                var fileName = inputParts[2];

                var importer = Importer.GetImporter(fileName);
                importer.Import(fileName);
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