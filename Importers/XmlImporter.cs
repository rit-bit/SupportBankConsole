using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NLog;

namespace SupportBankConsole.Importers
{
    public class XmlImporter : Importer
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        IEnumerable<Transaction> Importer.ImportFromFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            string contents;
            using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
            {
                contents = streamReader.ReadToEnd();
            }
            doc.LoadXml(contents);
            
            XmlElement root = doc.DocumentElement;
            IEnumerator ienum = root.GetEnumerator();
            var i = 0;
            while (ienum.MoveNext())
            {
                var SupportTransaction = doc.GetElementsByTagName("SupportTransaction");
                XmlNodeList to = doc.GetElementsByTagName("To");
                XmlNodeList from = doc.GetElementsByTagName("From");
                XmlNodeList narrative = doc.GetElementsByTagName("Description");
                XmlNodeList amount = doc.GetElementsByTagName("Value");

                string date = DateTime.Now.ToString();
                var TransactionElement = (XmlElement) SupportTransaction[i];
                if (TransactionElement.HasAttribute("Date"))
                { date = TransactionElement.GetAttribute("Date");
                }
                var toPerson = Person.GetOrCreatePerson(to[i].InnerXml);
                var fromPerson = Person.GetOrCreatePerson(from[i].InnerXml);
                var amountDecimal = Conversions.ConvertStringToDecimal(amount[i].InnerXml);
                var dateTime = Conversions.ConvertXMLStringToDate(date);
                yield return new Transaction(dateTime, fromPerson, toPerson, narrative[i].InnerXml, amountDecimal);
                i++;

            }
        }
    }
}