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
            if (firstName == null)
            {
                throw new ArgumentNullException(nameof(firstName), "First name not should be null");
            }
            if (lastName == null)
            {
                throw new ArgumentNullException(nameof(lastName), "Last name not should be null");
            }

            var record = new FileCabinetRecord();

            if (!string.IsNullOrEmpty(firstName) && firstName.Length >= 2 && firstName.Length <= 60)
            {
                record.FirstName = firstName;
            }
            else
            {
                throw new ArgumentException("First name less than two or more than 60 characters", nameof(firstName));
            }

            if (!string.IsNullOrEmpty(lastName) && lastName.Length >= 2 && lastName.Length <= 60)
            {
                record.LastName = lastName;
            }
            else
            {
                throw new ArgumentException("Last name less than two or more than 60 characters", nameof(lastName));
            }

            if (dateOfBirth >= new DateTime(1950, 1, 1) && dateOfBirth <= DateTime.Now)
            {
                record.DateOfBirth = dateOfBirth;
            }
            else
            {
                throw new ArgumentException("Input correct date", nameof(dateOfBirth));
            }

            if (age >= 12 && age <= 99)
            {
                record.Age = age;
            }
            else
            {
                throw new ArgumentException("Age less than 12 or more than 99 years", nameof(age));
            }

            if (money >= 0)
            {
                record.Money = money;
            }
            else
            {
                throw new ArgumentException("Money must be greater or equal to zero", nameof(money));
            }

            record.Letter = letter;
            record.Id = list.Count + 1;

            list.Add(record);

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
