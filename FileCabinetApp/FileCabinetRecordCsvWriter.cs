using System.IO;

namespace FileCabinetApp
{
    internal class FileCabinetRecordCsvWriter
    {
        private TextWriter writer;

        public FileCabinetRecordCsvWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void WriteString(string input)
        {
            writer.WriteLine(input);
        }

        public void Write(FileCabinetRecord record)
        {
            writer.WriteLine($"{record.Id},{record.FirstName},{record.LastName},{record.DateOfBirth},{record.Age},{record.Money},{record.Letter}");
        }
    }
}
