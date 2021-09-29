using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

namespace SupportBankConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactions = importCSV();
        }
        
        public static List<Transaction> importCSV()
        {
            string line;
            FileStream aFile = new FileStream("./Transactions2014.csv", FileMode.Open);
            StreamReader sr = new StreamReader(aFile);

            var transactions = new List<Transaction>();
            // read data in line by line
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
                var parts = line.Split(",");
                // Console.WriteLine(parts[0]);
                var amount = Convert.ToDecimal(parts[4]);
                transactions.Add(new Transaction(parts[0], parts[1], parts[2], parts[3], amount));
            }
            sr.Close();
            return transactions;
        }
    }
}