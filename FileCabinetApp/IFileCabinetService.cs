using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Represent record service type.
    /// </summary>
    internal interface IFileCabinetService
    {
        /// <summary>
        /// Represent create of record.
        /// </summary>
        /// <param name="record">Record info of the person.</param>
        /// <returns>Identifier of person.</returns>
        public int CreateRecord(FileCabinetRecord record);

        /// <summary>
        /// Represent edit of the record.
        /// </summary>
        /// <param name="id">Identifier of the person.</param>
        /// <param name="newRecord">Record info of the person.</param>
        public void EditRecord(int id, FileCabinetRecord newRecord);

        /// <summary>
        /// Find records by first name.
        /// </summary>
        /// <param name="firstName">Person's name.</param>
        /// <returns>Array of records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName);

        /// <summary>
        /// Find records by last name.
        /// </summary>
        /// <param name="lastName">Person's surname.</param>
        /// <returns>Array of records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName);

        /// <summary>
        /// Find records by date of birth.
        /// </summary>
        /// <param name="inputDateOfBirth">Person's date of birth.</param>
        /// <returns>Array of records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(string inputDateOfBirth);

        /// <summary>
        /// Represent all records.
        /// </summary>
        /// <returns>Array of records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords();

        /// <summary>
        /// Represent count of records.
        /// </summary>
        /// <returns>Count of records.</returns>
        public int GetStat();
    }
}
