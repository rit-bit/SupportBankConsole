using System;

namespace SupportBankConsole
{
    public class Transaction
    {
        private readonly string Date;
        private readonly string From;
        private readonly string To;
        private readonly string Narrative;
        private readonly int Amount;

        public Transaction(string date, string from, string to, string narrative, int amount)
        {
            Date = date;
            From = from;
            To = to;
            Narrative = narrative;
            Amount = amount;
        }
    }
}