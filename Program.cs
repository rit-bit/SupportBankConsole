using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Transactions;

namespace SupportBankConsole
{
    class Program
    {
        
        List<Person> people = new List<Person>();
        static void Main(string[] args)
        {
            var p = new Program();
            var transactions = p.importCSV();
            
        }
        
        public List<Transaction> importCSV()
        {
            string line;
            FileStream aFile = new FileStream("./Transactions2014.csv", FileMode.Open);
            StreamReader sr = new StreamReader(aFile);

            var transactions = new List<Transaction>();

            // read data in line by line
            line = sr.ReadLine(); // Remove the header from the CSV
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
                var parts = line.Split(",");
                // Console.WriteLine(parts[0]);
                
                var date = DateTime.ParseExact(parts[0], "d/M/yyyy", CultureInfo.InvariantCulture); // ADDED
                var from = getPerson(parts[1]);
                var to = getPerson(parts[2]);
                var narrative = parts[3];
                var amount = Convert.ToDecimal(parts[4]);
                
                transactions.Add(new Transaction(date, from, to, narrative, amount));
            }
            sr.Close();
            return transactions;
            
            
        }

        public  Person getPerson(string name)
        {
            foreach (var x in people)
            {
                if (x.name == name)
                {
                    return x;
                }
            }

            var p = new Person(name);
            people.Add(p);
            return p;
        }
        
    }
}