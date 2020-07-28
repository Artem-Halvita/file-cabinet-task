using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Represent the model of the record.
    /// </summary>
    public class FileCabinetRecord
    {
        /// <summary>
        /// Gets or sets represents person's identify.
        /// </summary>
        /// <value>
        /// Person's identify.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets person's first name.
        /// </summary>
        /// <value>
        /// Person's first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets person's last name.
        /// </summary>
        /// <value>
        /// Person's last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets person's date of birth.
        /// </summary>
        /// <value>
        /// Person's date of birth.
        /// </value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets person's age.
        /// </summary>
        /// <value>
        /// Person's age.
        /// </value>
        public short Age { get; set; }

        /// <summary>
        /// Gets or sets person's money.
        /// </summary>
        /// <value>
        /// Person's money.
        /// </value>
        public decimal Money { get; set; }

        /// <summary>
        /// Gets or sets person's letter.
        /// </summary>
        /// <value>
        /// Person's letter.
        /// </value>
        public char Letter { get; set; }
    }
}
