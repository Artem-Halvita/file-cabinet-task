using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Default custom settrings.
    /// </summary>
    internal class DefaultValidator : IRecordValidator
    {
        /// <summary>
        /// Validate paprameters.
        /// </summary>
        /// <param name="record">Person's record.</param>
        public void ValidateParameters(FileCabinetRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record), "Argument is null");
            }

            if (string.IsNullOrWhiteSpace(record.FirstName) || record.FirstName.Length < 2 || record.FirstName.Length > 60)
            {
                throw new ArgumentException("First name less than 2 or more than 60 characters");
            }

            if (string.IsNullOrWhiteSpace(record.LastName) || record.LastName.Length < 2 || record.LastName.Length > 60)
            {
                throw new ArgumentException("Last name less than 2 or more than 60 characters");
            }

            if (record.DateOfBirth <= new DateTime(1950, 1, 1) || record.DateOfBirth >= DateTime.Now)
            {
                throw new ArgumentException("Input correct date");
            }

            if (record.Age <= 12 || record.Age >= 99)
            {
                throw new ArgumentException("Age less than 12 or more than 99 years");
            }

            if (record.Money < 0)
            {
                throw new ArgumentException("Money must be greater or equal to zero");
            }

            if (!char.IsLetter(record.Letter))
            {
                throw new ArgumentException("Must be a letter", nameof(record.Letter));
            }
        }
    }
}
