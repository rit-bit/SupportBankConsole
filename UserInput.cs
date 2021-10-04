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
                    var name = input[5..];
                    program.ListAccount(name);
                }
            }
            
            if (inputParts[0] == "Import" && inputParts[1] == "File" && inputParts.Length == 3)
            {
                var fileName = inputParts[2];

                var importer = IImporter.GetImporter(fileName);
                importer.Import(fileName);
            }
            
            if (inputParts[0] == "Export" && inputParts[1] == "File" && inputParts.Length == 3)
            {
                var fileName = inputParts[2];
                
                var exporter = IExporter.GetExporter(fileName);
                exporter.Export(fileName);
            }
            
            
        }
        private static string RequestUserInput()
        {
            Console.WriteLine("\nThese are the available commands: ");
            Console.WriteLine("- List All");
            Console.WriteLine("- List [Account] where [Account] is a name");
            Console.WriteLine("- Import File [Filename] where [Filename] is the files name");
            Console.WriteLine("- Export File [Filename] where [Filename] is what you want the file to be saved as");
            Console.WriteLine("Please enter one of the commands: ");
            Logger.Info("Requested the user to input a command");
            return Console.ReadLine();
        }
    }
}