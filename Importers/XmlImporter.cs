using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using NLog;

namespace SupportBankConsole.Importers
{
    public class XmlImporter : IImporter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        IEnumerable<Transaction> IImporter.ImportFromFile(string path)
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
                var supportTransaction = doc.GetElementsByTagName("SupportTransaction");
                XmlNodeList to = doc.GetElementsByTagName("To");
                XmlNodeList from = doc.GetElementsByTagName("From");
                XmlNodeList narrative = doc.GetElementsByTagName("Description");
                XmlNodeList amount = doc.GetElementsByTagName("Value");

                string date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                var transactionElement = (XmlElement) supportTransaction[i];
                if (transactionElement != null && transactionElement.HasAttribute("Date"))
                { date = transactionElement.GetAttribute("Date");
                }
                var toPerson = Person.GetOrCreatePerson(to[i].InnerXml);
                var fromPerson = Person.GetOrCreatePerson(from[i].InnerXml);
                var amountDecimal = Conversions.ConvertStringToDecimal(amount[i].InnerXml);
                var dateTime = ConvertXmlStringToDate(date);
                yield return new Transaction(dateTime, fromPerson, toPerson, narrative[i].InnerXml, amountDecimal);
                i++;

            }
        }

        private static DateTime ConvertXmlStringToDate(string dateString)
        {
            var dateNumber = Convert.ToInt32(dateString);
            var date = new DateTime(1900,01,01);
            date = date.AddDays(dateNumber);
            return date;
        }
    }
}