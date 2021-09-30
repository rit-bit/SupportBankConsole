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
        private readonly List<Person> _people = new List<Person>();
        private readonly List<Transaction> _transactions = new List<Transaction>();
        
        static void Main(string[] args)
        {
            var p = new Program();
            p.ImportCsv("./Transactions2014.csv");
            p.ImportCsv("./DodgyTransactions2015.csv");
            while (true)
            {
                p.UserInput();
            }
        }
        
        public List<Transaction> ImportCsv(string path)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            
            
            var aFile = new FileStream(path, FileMode.Open);
            var sr = new StreamReader(aFile);

            

            // read data in line by line
            var line = sr.ReadLine(); // Remove header from CSV
            while ((line = sr.ReadLine()) != null)
            {
                // Console.WriteLine(line);
                var parts = line.Split(",");
                // Console.WriteLine(parts[0]);
                Logger.Info($"Attempting to parse date {parts[0]}");
                var date = DateTime.ParseExact(parts[0], "d/M/yyyy", CultureInfo.InvariantCulture);
                Logger.Info($"Attempting to get person {parts[1]}");
                var from = GetPerson(parts[1]);
                Logger.Info($"Attempting to get person {parts[2]}");
                var to = GetPerson(parts[2]);
                Logger.Info($"Attempting to get narrative {parts[3]}");
                var narrative = parts[3];
                Logger.Info($"Attempting to get amount {parts[4]}");
                var amount = Convert.ToDecimal(parts[4]);
                var transaction = new Transaction(date, from, to, narrative, amount);
                _transactions.Add(transaction);
                from.DecreaseAmount(amount);
                to.IncreaseAmount(amount);
            }
            sr.Close();
            
            return _transactions;
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
            var sub = input[5..];
            switch (sub)
            {
                case ("All"):
                    ListAll();
                    break;
                default:
                    ListAccount(sub);
                    break;
            }

        }

        private void ListAll()
        {
            foreach (var person in _people)
            {
                Console.WriteLine(person);
            }
        }

        private void ListAccount(string name)
        {
            foreach (var transaction in _transactions)
            {
                if (transaction.From.Name == name)
                {
                    Console.WriteLine(transaction);
                }
            }
        }

        private Person GetPerson(string name)
        {
            foreach (var x in _people)
            {
                if (x.Name == name)
                {
                    return x;
                }
            }

            var p = new Person(name);
            _people.Add(p);
            return p;
        }
        
    }
}