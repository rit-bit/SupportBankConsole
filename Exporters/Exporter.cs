using System;
using System.Collections.Generic;
using SupportBankConsole.Importers;

namespace SupportBankConsole.Exporters
{
    public interface IExporter
    {
        
        public static IExporter GetExporter(string filename)
        {
            var index = filename.LastIndexOf(".", StringComparison.Ordinal);
            switch (filename[index..])
            {
                case ".csv":
                    return new CsvExporter();
                case ".json":
                    return new JsonExporter();
                default:
                    throw new NotSupportedException();
            }
        }

        protected void ExportToFile(string filename);

        public void Export(string filename)
        {
            ExportToFile(filename);
            Console.WriteLine($"File {filename} was exported successfully.");
        }
    }
}