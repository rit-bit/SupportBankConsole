using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NLog;

namespace SupportBankConsole
{
    public class ImportXML
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static void importXML(string path)
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
            while (ienum.MoveNext())
            {
                var SupportTransaction = doc.GetElementsByTagName("SupportTransaction");
                XmlNodeList to = doc.GetElementsByTagName("To");
                XmlNodeList from = doc.GetElementsByTagName("From");
                XmlNodeList narrative = doc.GetElementsByTagName("Description");
                XmlNodeList amount = doc.GetElementsByTagName("Value");
                for (int i=0; i < to.Count; i++)
                {
                    string date = DateTime.Now.ToString();
                    var TransactionElement = (XmlElement) SupportTransaction[i];
                    if (TransactionElement.HasAttribute("Date")){
                        date = TransactionElement.GetAttribute("Date");
                    }

                    var toPerson = Person.GetOrCreatePerson(to[i].InnerXml);
                    var fromPerson = Person.GetOrCreatePerson(from[i].InnerXml);
                    var amountDecimal = Conversions.ConvertStringToDecimal(amount[i].InnerXml);
                    
                    Console.WriteLine(date);
                    Console.WriteLine("");
                }
            }
        }
    }
}