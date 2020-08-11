using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace FileCabinetApp
{
    internal class FileCabinetFileSystemService : IFileCabinetService
    {
        private FileStream fileStream;

        public FileCabinetFileSystemService(FileStream fileStream)
        {
            this.fileStream = fileStream;
        }

        public int CreateRecord(FileCabinetRecord record)
        {
            throw new NotImplementedException();
        }

        public void EditRecord(int id, FileCabinetRecord newRecord)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(string inputDateOfBirth)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            throw new NotImplementedException();
        }

        public int GetStat()
        {
            throw new NotImplementedException();
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            throw new NotImplementedException();
        }
    }
}
