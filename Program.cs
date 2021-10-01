﻿using System;
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

        private void UserInput()
        {
            Console.WriteLine("These are the available commands: ");
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

            if (inputParts[0] == "Import" && inputParts[1] == "File")
            {
                var fileName = inputParts[2];
                var index = fileName.LastIndexOf(".", StringComparison.Ordinal);
                var extension = fileName.Substring(index);
                if (extension == ".csv")
                {
                    ImportCSV.ImportCsv($"./{fileName}");
                    Console.WriteLine($"Import of file {fileName} has been successful");
                }
                else if (extension == ".json")
                {
                  ImportJSON.ImportJson($"./{fileName}");
                  Console.WriteLine($"Import of file {fileName} has been successful");
                }
                else if (extension == ".xml")
                {
                    ImportXML.importXML($"./{fileName}");
                    Console.WriteLine($"Import of file {fileName} has been successful");
                }
                else
                {
                    var errorMessage = $"Could not read file {fileName}, Please try again";
                    Logger.Info(errorMessage);
                    Console.WriteLine(errorMessage);
                    return;
                }
                foreach (var transaction in Transaction.GetTransactions())
                {
                    transaction.From.DecreaseAmount(transaction.Amount);
                    transaction.To.IncreaseAmount(transaction.Amount);
                }
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