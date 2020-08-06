using System;
using System.Globalization;

namespace FileCabinetApp
{
    /// <summary>
    /// Represent validate types.
    /// </summary>
    internal interface IRecordValidator
    {
        /// <summary>
        /// Validate first name.
        /// </summary>
        /// <param name="firstName">Person's first name.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> FirstNameValidator(string firstName);

        /// <summary>
        /// Validate last name.
        /// </summary>
        /// <param name="lastName">Person's last name.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> LastNameValidator(string lastName);

        /// <summary>
        /// Validate date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Person's date of birth.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> DateOfBirthValidator(DateTime dateOfBirth);

        /// <summary>
        /// Validate age.
        /// </summary>
        /// <param name="age">Person's age.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> AgeValidator(short age);

        /// <summary>
        /// Validate money.
        /// </summary>
        /// <param name="money">Person's money.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> MoneyValidator(decimal money);

        /// <summary>
        /// Validate letter.
        /// </summary>
        /// <param name="letter">Person's letter.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> LetterValidator(char letter);
    }
}
