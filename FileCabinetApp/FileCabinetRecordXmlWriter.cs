using System;
using System.Globalization;
using System.Xml.Linq;

namespace FileCabinetApp
{
    internal class FileCabinetRecordXmlWriter
    {
        private XDocument document;

        public FileCabinetRecordXmlWriter(XDocument document)
        {
            this.document = document;
        }

        public void Write(FileCabinetRecord record)
        {
            document.Root.Add(new XElement("record", new XAttribute("id", record.Id),
                                new XElement("dateOfBirth", record.DateOfBirth.ToString("yyyy-MMM-dd", new CultureInfo("en-US"))),
                                new XElement("age", record.Age),
                                new XElement("money", record.Money),
                                new XElement("letter", record.Letter)));
        }
    }
}
