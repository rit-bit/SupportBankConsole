using NLog;

namespace SupportBankConsole
{
    public class Person
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public readonly string Name;
        private decimal _amount;

        public Person(string name)
        {
            Name = name;
        }

        public void IncreaseAmount(decimal amount)
        {
            _amount += amount;
        }

        public void DecreaseAmount(decimal amount)
        {
            _amount -= amount;
        }

        public override string ToString()
        {
            if (_amount < 0)
            {
                return ($"{Name} owes £{-_amount}");
            }

            return ($"{Name} is owed £{_amount}");
        }
    }
}