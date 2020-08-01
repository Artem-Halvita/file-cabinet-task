using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Represent validate types.
    /// </summary>
    internal interface IRecordValidator
    {
        /// <summary>
        /// Represent validate parameters.
        /// </summary>
        /// <param name="record">Person's record.</param>
        public void ValidateParameters(FileCabinetRecord record);
    }
}
