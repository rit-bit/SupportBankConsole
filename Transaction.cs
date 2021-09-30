using System;
using NLog;

namespace SupportBankConsole
{
    public class Transaction
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly DateTime Date; 
        public readonly Person From; 
        public readonly Person To; 
        private readonly string Narrative;
        private readonly decimal Amount;

        public Transaction(DateTime date, Person from, Person to, string narrative, decimal amount)
        {
            Date = date;
            From = from;
            To = to;
            Narrative = narrative;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Date:MM/dd/yyyy} {To.Name} lent £{Amount} to {From.Name} for {Narrative}.";
        }
    }
}