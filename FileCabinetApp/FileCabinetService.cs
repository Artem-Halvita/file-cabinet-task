using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

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

            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 60)
            {
                throw new ArgumentException("First name less than two or more than 60 characters", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 60)
            {
                throw new ArgumentException("Last name less than two or more than 60 characters", nameof(lastName));
            }

            if (dateOfBirth <= new DateTime(1950, 1, 1) || dateOfBirth >= DateTime.Now)
            {
                throw new ArgumentException("Input correct date", nameof(dateOfBirth));
            }

            if (age <= 12 || age >= 99)
            {
                throw new ArgumentException("Age less than 12 or more than 99 years", nameof(age));
            }

            if (money <= 0)
            {
                throw new ArgumentException("Money must be greater or equal to zero", nameof(money));
            }

            if (!char.IsLetter(letter))
            {
                throw new ArgumentException("Must be a letter", nameof(letter));
            }

            var record = new FileCabinetRecord()
            {
                Id = list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Age = age,
                Money = money,
                Letter = letter,
            };

            list.Add(record);

            if (!firstNameDictionary.ContainsKey(firstName))
            {
                firstNameDictionary.Add(firstName, list.FindAll(i => i.FirstName == firstName));
            }
            else
            {
                firstNameDictionary[firstName].Add(record);
            }

            if (!lastNameDictionary.ContainsKey(lastName))
            {
                lastNameDictionary.Add(lastName, list.FindAll(i => i.LastName == lastName));
            }
            else
            {
                lastNameDictionary[lastName].Add(record);
            }

            if (!dateOfBirthDictionary.ContainsKey(dateOfBirth))
            {
                dateOfBirthDictionary.Add(dateOfBirth, list.FindAll(i => i.DateOfBirth == dateOfBirth));
            }
            else
            {
                dateOfBirthDictionary[dateOfBirth].Add(record);
            }

            return record.Id;
        }
        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, short age, decimal money, char letter)
        {
            if (GetStat() < id)
            {
                throw new ArgumentException("Not exist", nameof(id));
            }

            var oldRecord = list.FindLast(i => i.Id == id);

            list.RemoveAt(id - 1);
            firstNameDictionary[oldRecord.FirstName].Remove(oldRecord);

            var newRecord = new FileCabinetRecord()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Age = age,
                Money = money,
                Letter = letter
            };

            list.Insert(id - 1, newRecord);    

            if (!firstNameDictionary.ContainsKey(firstName))
            {
                firstNameDictionary.Add(firstName, list.FindAll(i => i.FirstName == firstName));
            }
            else
            {
                firstNameDictionary[firstName].Add(newRecord);
            }

            if (!lastNameDictionary.ContainsKey(lastName))
            {
                lastNameDictionary.Add(lastName, list.FindAll(i => i.LastName == lastName));
            }
            else
            {
                lastNameDictionary[lastName].Add(newRecord);
            }

            if (!dateOfBirthDictionary.ContainsKey(dateOfBirth))
            {
                dateOfBirthDictionary.Add(dateOfBirth, list.FindAll(i => i.DateOfBirth == dateOfBirth));
            }
            else
            {
                dateOfBirthDictionary[dateOfBirth].Add(newRecord);
            }
        }
        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            return firstNameDictionary[firstName].ToArray();
        }
        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            return lastNameDictionary[lastName].ToArray();
        }
        public FileCabinetRecord[] FindByDateOfBirth(string inputDateOfBirth)
        {
            DateTime dateOfBirth;
            bool isParsed = DateTime.TryParse(inputDateOfBirth, out dateOfBirth);

            return dateOfBirthDictionary[dateOfBirth].ToArray();
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
