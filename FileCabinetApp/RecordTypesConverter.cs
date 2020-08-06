using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Represent converter types of FileCabinetRecord.
    /// </summary>
    internal class RecordTypesConverter
    {
        /// <summary>
        /// Convert string to correct string.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Tuple with convert info.</returns>
        public Tuple<bool, string, string> StringConverter(string input)
        {
            var convertTuple = Tuple.Create<bool, string, string>(false, input, default);

            if (!string.IsNullOrWhiteSpace(input))
            {
                convertTuple = Tuple.Create(true, input, input);
            }

            return convertTuple;
        }

        /// <summary>
        /// Convert string to DateTime.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Tuple with convert info.</returns>
        public Tuple<bool, string, DateTime> DateTimeConverter(string input)
        {
            var convertTuple = Tuple.Create<bool, string, DateTime>(false, input, default);

            if (DateTime.TryParse(input, out DateTime dateTime))
            {
                convertTuple = Tuple.Create(true, input, dateTime);
            }

            return convertTuple;
        }

        /// <summary>
        /// Convert string to short.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Tuple with convert info.</returns>
        public Tuple<bool, string, short> ShortConverter(string input)
        {
            var convertTuple = Tuple.Create<bool, string, short>(false, input, default);

            if (short.TryParse(input, out short age))
            {
                convertTuple = Tuple.Create(true, input, age);
            }

            return convertTuple;
        }

        /// <summary>
        /// Convert string to decimal.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Tuple with convert info.</returns>
        public Tuple<bool, string, decimal> DecimalConverter(string input)
        {
            var convertTuple = Tuple.Create<bool, string, decimal>(false, input, default);

            if (decimal.TryParse(input, out decimal money))
            {
                convertTuple = Tuple.Create(true, input, money);
            }

            return convertTuple;
        }

        /// <summary>
        /// Convert string to char.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Tuple with convert info.</returns>
        public Tuple<bool, string, char> CharConverter(string input)
        {
            var convertTuple = Tuple.Create<bool, string, char>(false, input, default);

            if (char.TryParse(input, out char letter))
            {
                convertTuple = Tuple.Create(true, input, letter);
            }

            return convertTuple;
        }
    }
}