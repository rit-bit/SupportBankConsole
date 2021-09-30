using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Transactions;

namespace SupportBankConsole
{
    class Program
    {
        
        private readonly List<Person> _people = new List<Person>();
        private readonly List<Transaction> _transactions = new List<Transaction>();
        
        static void Main(string[] args)
        {
            var p = new Program();
            var transactions = p.ImportCsv();
            while (true)
            {
                p.UserInput();
            }
        }
        
        public List<Transaction> ImportCsv()
        {
            var aFile = new FileStream("./Transactions2014.csv", FileMode.Open);
            var sr = new StreamReader(aFile);

            

            // read data in line by line
            var line = sr.ReadLine(); // Remove header from CSV
            while ((line = sr.ReadLine()) != null)
            {
                // Console.WriteLine(line);
                var parts = line.Split(",");
                // Console.WriteLine(parts[0]);
                
                var date = DateTime.ParseExact(parts[0], "d/M/yyyy", CultureInfo.InvariantCulture); // ADDED
                var from = GetPerson(parts[1]);
                var to = GetPerson(parts[2]);
                var narrative = parts[3];
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
            var s = Console.ReadLine();
            var first = s.Substring(0, 5);
            if (first != "List ")
            {
                return;
            }
            var sub = s[5..];
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
            foreach (var y in _people)
            {
                Console.WriteLine(y);
            }
        }

        private void ListAccount(string name)
        {
            foreach (var transaction in _transactions)
            {
                if (transaction.From.name == name)
                {
                    Console.WriteLine(transaction);
                }
            }
        }

        private Person GetPerson(string name)
        {
            foreach (var x in _people)
            {
                if (x.name == name)
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