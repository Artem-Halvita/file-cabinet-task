using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Represent creating custom service.
    /// </summary>
    internal class FileCabinetCustomService : FileCabinetService
    {
        /// <summary>
        /// Create validator.
        /// </summary>
        /// <returns>Custom validator type.</returns>
        protected override IRecordValidator CreateValidator()
        {
            return new CustomValidator();
        }
    }
}
