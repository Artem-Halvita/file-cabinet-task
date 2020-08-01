using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Represent creating default service.
    /// </summary>
    internal class FileCabinetDefaultService : FileCabinetService
    {
        /// <summary>
        /// Create validator.
        /// </summary>
        /// <returns>Default validator type.</returns>
        protected override IRecordValidator CreateValidator()
        {
            return new DefaultValidator();
        }
    }
}
