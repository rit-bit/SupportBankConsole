using System;

namespace SupportBankConsole
{
    public class Transaction
    {
        private readonly string Date; // Change this to DateTime
        private readonly string From; // Change this to Person
        private readonly string To; // Change this to Person
        private readonly string Narrative;
        private readonly decimal Amount;

        public Transaction(string date, string from, string to, string narrative, decimal amount)
        {
            Date = date;
            From = from;
            To = to;
            Narrative = narrative;
            Amount = amount;
        }
    }
}