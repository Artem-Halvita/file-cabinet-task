using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, short age, decimal money, char letter)
        {
            var record = new FileCabinetRecord
            {
                Id = list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Age = age,
                Money = money,
                Letter = letter
            };

            this.list.Add(record);

            return record.Id;
        }
        public FileCabinetRecord[] GetRecords()
        {
            return list.ToArray();
        }
        public int GetStat()
        {
            return this.list.Count;
        }
    }
}
