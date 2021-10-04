namespace SupportBankConsole
{
    public interface ICommands
    {
        public void ListAll();

        public void ListAccount(string name);
        void TryToImportFile(string inputPart);
        void TryToExportFile(string inputPart);
    }
}