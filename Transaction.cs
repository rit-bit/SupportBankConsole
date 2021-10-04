using System;
using System.Collections.Generic;
using NLog;

namespace SupportBankConsole
{
    public class Transaction
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static List<Transaction> _transactions = new List<Transaction>();
        public DateTime Date { get; }
        public Person From { get; }
        public Person To { get; }
        public string Narrative{ get; }
        public decimal Amount{ get; }

        public Transaction(DateTime date, Person from, Person to, string narrative, decimal amount)
        {
            Date = date;
            From = from;
            To = to;
            Narrative = narrative;
            Amount = amount;
            _transactions.Add(this);
        }

        public static List<Transaction> GetTransactions()
        {
            return _transactions;
        }

        public override string ToString()
        {
            return $"{Date:MM/dd/yyyy} {To.Name} lent £{Amount:N2} to {From.Name} for {Narrative}.";
        }
    }
}