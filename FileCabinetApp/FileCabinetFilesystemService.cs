using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    internal class FileCabinetFileSystemService : IFileCabinetService
    {
        private FileStream fileStream;

        public FileCabinetFileSystemService(FileStream fileStream)
        {
            this.fileStream = fileStream;
        }

        public int CreateRecord(FileCabinetRecord record)
        {
            fileStream.Write(new byte[2], 0, 2);

            byte[] id = new byte[2];
            Encoding.Default.GetBytes(record.Id.ToString()).CopyTo(id, 0);
            fileStream.Write(id, 0, id.Length);

            byte[] firstName = new byte[120];
            Encoding.Default.GetBytes(record.FirstName).CopyTo(firstName, 0);
            fileStream.Write(firstName, 0, firstName.Length);

            byte[] lastName = new byte[120];
            Encoding.Default.GetBytes(record.LastName).CopyTo(lastName, 0);
            fileStream.Write(lastName, 0, lastName.Length);

            byte[] year = new byte[4];
            Encoding.Default.GetBytes(record.DateOfBirth.Year.ToString()).CopyTo(year, 0);
            fileStream.Write(year, 0, year.Length);

            byte[] month = new byte[4];
            Encoding.Default.GetBytes(record.DateOfBirth.Month.ToString()).CopyTo(month, 0);
            fileStream.Write(month, 0, month.Length);

            byte[] day = new byte[4];
            Encoding.Default.GetBytes(record.DateOfBirth.Day.ToString()).CopyTo(day, 0);
            fileStream.Write(day, 0, day.Length);

            byte[] age = new byte[2];
            Encoding.Default.GetBytes(record.Age.ToString()).CopyTo(age, 0);
            fileStream.Write(age, 0, age.Length);

            byte[] money = new byte[16];
            Encoding.Default.GetBytes(record.Money.ToString()).CopyTo(money, 0);
            fileStream.Write(money, 0, money.Length);

            byte[] letter = new byte[2];
            Encoding.Default.GetBytes(record.Letter.ToString()).CopyTo(letter, 0);
            fileStream.Write(letter, 0, letter.Length);

            fileStream.Close();

            return record.Id;
        }

        public void EditRecord(int id, FileCabinetRecord newRecord)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(string inputDateOfBirth)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            throw new NotImplementedException();
        }

        public int GetStat()
        {
            throw new NotImplementedException();
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            throw new NotImplementedException();
        }
    }
}
