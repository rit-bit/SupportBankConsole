using System.Collections.Generic;
using NLog;

namespace SupportBankConsole
{
    public class Person
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static List<Person> _people = new List<Person>();
        public readonly string Name;
        private decimal _amount;

        public Person(string name)
        {
            Name = name;
            _people.Add(this);
        }

        public static List<Person> GetPeople()
        {
            return _people;
        }
        
        public static Person GetOrCreatePerson(string name)
        {
            foreach (var x in _people)
            {
                if (x.Name == name)
                {
                    return x;
                }
            }

            return new Person(name);
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