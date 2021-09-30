using System;

namespace SupportBankConsole
{
    public class Transaction
    {
        private readonly DateTime Date; 
        private readonly Person From; 
        private readonly Person To; 
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
    }
}