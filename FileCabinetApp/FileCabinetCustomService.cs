using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Custom service settings.
    /// </summary>
    internal class FileCabinetCustomService : FileCabinetService
    {
        /// <summary>
        /// Validate parameters.
        /// </summary>
        /// <param name="record">Person's record.</param>
        protected override void ValidateParameters(FileCabinetRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record), "Argument is null");
            }

            if (string.IsNullOrWhiteSpace(record.FirstName) || record.FirstName.Length < 3 || record.FirstName.Length > 10)
            {
                throw new ArgumentException("First name less than 3 or more than 10 characters");
            }

            if (string.IsNullOrWhiteSpace(record.LastName) || record.LastName.Length < 3 || record.LastName.Length > 20)
            {
                throw new ArgumentException("Last name less than 3 or more than 20 characters");
            }

            if (record.DateOfBirth <= new DateTime(1960, 1, 1) || record.DateOfBirth >= new DateTime(2010, 1, 1))
            {
                throw new ArgumentException("Input correct date");
            }

            if (record.Age <= 18 || record.Age >= 60)
            {
                throw new ArgumentException("Age less than 12 or more than 99 years");
            }

            if (record.Money < -100000)
            {
                throw new ArgumentException("Money must be greater than -100000");
            }

            if (!char.IsLetter(record.Letter))
            {
                throw new ArgumentException("Must be a letter");
            }
        }
    }
}
