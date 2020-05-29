using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth)
        {
            // TODO: Добавьте реализацию метода
            return 0;
        }
        public FileCabinetRecord[] GetRecords()
        {
            // TODO: Добавьте реализацию метода
            return new FileCabinetRecord[] { };
        }
        public int GetStat()
        {
            // TODO: Добавьте реалзиацию метода
            return 0;
        }
    }
}
