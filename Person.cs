namespace SupportBankConsole
{
    public class Person
    {
        public readonly string name;
        private decimal amount;

        public Person(string name)
        {
            this.name = name;
            this.amount = 0;
        }

        public void IncreaseAmount(decimal amount)
        {
            this.amount += amount;
        }

        public void DecreaseAmount(decimal amount)
        {
            this.amount -= amount;
        }

        public override string ToString()
        {
            if (this.amount < 0)
            {
                return ($"{name} owes £{-amount}");
            }

            return ($"{name} is owed £{amount}");
        }
    }
}