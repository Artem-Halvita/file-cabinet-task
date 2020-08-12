using System;
using CommandLine;

namespace FileCabinetApp
{
    internal class Options
    {
        [Option('v', "validation-rules")]
        public string Validation { get; set; }

        [Option('s', "storage")]
        public string Storage { get; set; }
    }
}
