using System;

namespace SupportBankConsole.Exporters
{
    public class TransactionToJson
    {
        public DateTime Date;
        public string FromAccount;
        public string ToAccount;
        public string Narrative;
        public decimal Amount;

        public TransactionToJson(Transaction transaction)
        {
            Date = transaction.Date;
            FromAccount = transaction.FromAccount.Name;
            ToAccount = transaction.ToAccount.Name;
            Narrative = transaction.Narrative;
            Amount = transaction.Amount;
        }
    }
}