using System;
using NLog;
using SupportBankConsole.Importers;

namespace SupportBankConsole
{
    public class UserInput
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static void GetUserInput()
        {
            var input = RequestUserInput();
            var inputParts = input.Split(" ");
            Logger.Info($"User inputted {input}");

            if (inputParts.Length <= 1)
            {
                return;
            }
            
            if (inputParts[0] == "List")
            {
                if(inputParts[1] == "All")
                {
                    ListAll();
                }
                else
                {
                    var name = input[5..];
                    ListAccount(name);
                }
            }
            
            if (inputParts[0] == "Import" && inputParts[1] == "File" && inputParts.Length == 3)
            {
                var fileName = inputParts[2];

                var importer = Importer.GetImporter(fileName);
                importer.Import(fileName);
            }
        }
        private static string RequestUserInput()
        {
            Console.WriteLine("\nThese are the available commands: ");
            Console.WriteLine("- List All");
            Console.WriteLine("- List [Account] where [Account] is a name");
            Console.WriteLine("- Import File [Filename] where [Filename] is the files name");
            Console.WriteLine("Please enter one of the commands: ");
            Logger.Info("Requested the user to input a command");
            return Console.ReadLine();
        }
    }
}