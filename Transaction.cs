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
        public Person FromAccount { get; }
        public Person ToAccount { get; }
        public string Narrative{ get; }
        public decimal Amount{ get; }

        public Transaction(DateTime date, Person fromAccount, Person toAccount, string narrative, decimal amount)
        {
            Date = date;
            FromAccount = fromAccount;
            ToAccount = toAccount;
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
            return $"{Date:MM/dd/yyyy} {ToAccount.Name} lent £{Amount:N2} to {FromAccount.Name} for {Narrative}.";
        }
    }
}