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
    }
}