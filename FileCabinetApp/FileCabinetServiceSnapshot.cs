using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace FileCabinetApp
{
    internal class FileCabinetServiceSnapshot
    {
        private FileCabinetRecord[] records; // Maybe cast to List<RileCabinetService> ?

        public FileCabinetServiceSnapshot(List<FileCabinetRecord> list)
        {
            records = list.ToArray();
        }

        public void SaveToCsv(StreamWriter streamWriter)
        {
            var csvWriter = new FileCabinetRecordCsvWriter(streamWriter);

            csvWriter.WriteString("Id,First name,Last name,Date of birth,Age,Money,Letter");

            foreach (var record in records)
            {
                csvWriter.Write(record);
            }
        }

        public void SaveToXml(StreamWriter streamWriter)
        {
            var document = new XDocument(new XElement("records"));
            var xmlWriter = new FileCabinetRecordXmlWriter(document);

            foreach (var record in records)
            {
                xmlWriter.Write(record);
            }

            document.Save(streamWriter);
        }
    }
}
