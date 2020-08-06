using System;
using System.Globalization;

namespace FileCabinetApp
{
    /// <summary>
    /// Default custom settrings.
    /// </summary>
    internal class DefaultValidator : IRecordValidator
    {
        private static CultureInfo cultureInfo = new CultureInfo("en-US");

        /// <summary>
        /// Validate first name.
        /// </summary>
        /// <param name="firstName">Person's first name.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> FirstNameValidator(string firstName)
        {
            var firstNameTuple = Tuple.Create(false, firstName);

            if (firstName.Length > 2 && firstName.Length < 60)
            {
                firstNameTuple = Tuple.Create(true, firstName);
            }

            return firstNameTuple;
        }

        /// <summary>
        /// Validate last name.
        /// </summary>
        /// <param name="lastName">Person's last name.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> LastNameValidator(string lastName)
        {
            var lastNameTuple = Tuple.Create(false, lastName);

            if (lastName.Length > 2 && lastName.Length < 60)
            {
                lastNameTuple = Tuple.Create(true, lastName);
            }

            return lastNameTuple;
        }

        /// <summary>
        /// Validate date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Person's date of birth.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> DateOfBirthValidator(DateTime dateOfBirth)
        {
            string dateOfBirthString = Convert.ToString(dateOfBirth, cultureInfo);
            var dateOfBirthTuple = Tuple.Create(false, dateOfBirthString);

            if (dateOfBirth > new DateTime(1950, 1, 1) && dateOfBirth < DateTime.Now)
            {
                dateOfBirthTuple = Tuple.Create(true, dateOfBirthString);
            }

            return dateOfBirthTuple;
        }

        /// <summary>
        /// Validate age.
        /// </summary>
        /// <param name="age">Person's age.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> AgeValidator(short age)
        {
            string ageString = Convert.ToString(age, cultureInfo);
            var ageTuple = Tuple.Create(false, ageString);

            if (age > 12 || age < 99)
            {
                ageTuple = Tuple.Create(true, ageString);
            }

            return ageTuple;
        }

        /// <summary>
        /// Validate money.
        /// </summary>
        /// <param name="money">Person's money.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> MoneyValidator(decimal money)
        {
            string moneyString = Convert.ToString(money, cultureInfo);
            var moneyTuple = Tuple.Create(false, moneyString);

            if (money > 0)
            {
                moneyTuple = Tuple.Create(true, moneyString);
            }

            return moneyTuple;
        }

        /// <summary>
        /// Validate letter.
        /// </summary>
        /// <param name="letter">Person's letter.</param>
        /// <returns>Tuple with validation info.</returns>
        public Tuple<bool, string> LetterValidator(char letter)
        {
            string letterString = Convert.ToString(letter, cultureInfo);
            var letterTuple = Tuple.Create(false, letterString);

            if (char.IsLetter(letter))
            {
                letterTuple = Tuple.Create(true, letterString);
            }

            return letterTuple;
        }
    }
}
