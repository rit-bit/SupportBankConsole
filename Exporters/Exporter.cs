using System;
using System.Collections.Generic;
using NLog;
using SupportBankConsole.Importers;

namespace SupportBankConsole.Exporters
{
    public interface IExporter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static IExporter GetExporter(string filename)
        {
            var index = filename.LastIndexOf(".", StringComparison.Ordinal);
            var extension = filename[index..];
            switch (extension)
            {
                case ".csv":
                    return new CsvExporter();
                case ".json":
                    return new JsonExporter();
                default:
                    var msg = $"\"{extension}\" file extension is not supported for exporting.";
                    Logger.Error(msg);
                    throw new NotSupportedException(msg);
            }
        }

        protected void ExportToFile(string filename);

        public void Export(string filename)
        {
            ExportToFile(filename);
            var msg = $"File {filename} was exported successfully.";
            Console.WriteLine(msg);
            Logger.Info(msg);
        }
    }
}