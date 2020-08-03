using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Represent record service.
    /// </summary>
    internal class FileCabinetService : IFileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();
        private IRecordValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetService"/> class.
        /// </summary>
        /// <param name="validator">Type of validator.</param>
        public FileCabinetService(IRecordValidator validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Represent create of record.
        /// </summary>
        /// <param name="record">Record info of the person.</param>
        /// <returns>Identifier of person.</returns>
        public int CreateRecord(FileCabinetRecord record)
        {
            validator.ValidateParameters(record);

            record.Id = list.Count + 1;

            list.Add(record);

            if (!firstNameDictionary.ContainsKey(record.FirstName))
            {
                firstNameDictionary.Add(record.FirstName, list.FindAll(i => i.FirstName == record.FirstName));
            }
            else
            {
                firstNameDictionary[record.FirstName].Add(record);
            }

            if (!lastNameDictionary.ContainsKey(record.LastName))
            {
                lastNameDictionary.Add(record.LastName, list.FindAll(i => i.LastName == record.LastName));
            }
            else
            {
                lastNameDictionary[record.LastName].Add(record);
            }

            if (!dateOfBirthDictionary.ContainsKey(record.DateOfBirth))
            {
                dateOfBirthDictionary.Add(record.DateOfBirth, list.FindAll(i => i.DateOfBirth == record.DateOfBirth));
            }
            else
            {
                dateOfBirthDictionary[record.DateOfBirth].Add(record);
            }

            return record.Id;
        }

        /// <summary>
        /// Represent edit of the record.
        /// </summary>
        /// <param name="id">Identifier of the person.</param>
        /// <param name="newRecord">Record info of the person.</param>
        public void EditRecord(int id, FileCabinetRecord newRecord)
        {
            if (GetStat() < id)
            {
                throw new ArgumentException("Not exist", nameof(id));
            }

            validator.ValidateParameters(newRecord);

            var oldRecord = list.FindLast(i => i.Id == id);

            list.RemoveAt(id - 1);
            firstNameDictionary[oldRecord.FirstName].Remove(oldRecord);

            newRecord.Id = id;

            list.Insert(id - 1, newRecord);

            if (!firstNameDictionary.ContainsKey(newRecord.FirstName))
            {
                firstNameDictionary.Add(newRecord.FirstName, list.FindAll(i => i.FirstName == newRecord.FirstName));
            }
            else
            {
                firstNameDictionary[newRecord.FirstName].Add(newRecord);
            }

            if (!lastNameDictionary.ContainsKey(newRecord.LastName))
            {
                lastNameDictionary.Add(newRecord.LastName, list.FindAll(i => i.LastName == newRecord.LastName));
            }
            else
            {
                lastNameDictionary[newRecord.LastName].Add(newRecord);
            }

            if (!dateOfBirthDictionary.ContainsKey(newRecord.DateOfBirth))
            {
                dateOfBirthDictionary.Add(newRecord.DateOfBirth, list.FindAll(i => i.DateOfBirth == newRecord.DateOfBirth));
            }
            else
            {
                dateOfBirthDictionary[newRecord.DateOfBirth].Add(newRecord);
            }
        }

        /// <summary>
        /// Find records by first name.
        /// </summary>
        /// <param name="firstName">Person's name.</param>
        /// <returns>Array of records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            return firstNameDictionary[firstName].AsReadOnly();
        }

        /// <summary>
        /// Find records by last name.
        /// </summary>
        /// <param name="lastName">Person's surname.</param>
        /// <returns>Array of records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            return lastNameDictionary[lastName].AsReadOnly();
        }

        /// <summary>
        /// Find records by date of birth.
        /// </summary>
        /// <param name="inputDateOfBirth">Person's date of birth.</param>
        /// <returns>Array of records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(string inputDateOfBirth)
        {
            if (DateTime.TryParse(inputDateOfBirth, out DateTime dateOfBirth))
            {
                return dateOfBirthDictionary[dateOfBirth].AsReadOnly();
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Represent all records.
        /// </summary>
        /// <returns>Array of records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return list.AsReadOnly();
        }

        /// <summary>
        /// Represent count of records.
        /// </summary>
        /// <returns>Count of records.</returns>
        public int GetStat()
        {
            return this.list.Count;
        }
    }
}
