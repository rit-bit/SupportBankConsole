using System;
using NLog;
using SupportBankConsole.Exporters;
using SupportBankConsole.Importers;

namespace SupportBankConsole
{
    public static class UserInput
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static void GetUserInput(ICommands program)
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
                    program.ListAll();
                }
                else
                {
                    program.ListAccount(input[5..]);
                }
            }
            
            if (inputParts[0] == "Import" && inputParts[1] == "File" && inputParts.Length == 3)
            {
                program.TryToImportFile(inputParts[2]);
            }
            
            if (inputParts[0] == "Export" && inputParts[1] == "File" && inputParts.Length == 3)
            {
                program.TryToExportFile(inputParts[2]);
            }
        }
        private static string RequestUserInput()
        {
            Console.WriteLine("\nThese are the available commands: ");
            Console.WriteLine("- List All");
            Console.WriteLine("- List [Account] where [Account] is a name");
            Console.WriteLine("- Import File [Filename] where [Filename] is the files name");
            Console.WriteLine("- Export File [Filename] where [Filename] is the name of the output file to create");
            Console.WriteLine("All filenames should end with \".csv\", \".json\", or \".xml\"");
            Console.WriteLine("Please enter one of the commands: ");
            Logger.Info("Requested the user to input a command");
            return Console.ReadLine();
        }
    }
}