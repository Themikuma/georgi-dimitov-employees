﻿using Employees.Models;

namespace Employees.Interfaces
{
    public interface IDataIngestionService
    {
        /// <summary>
        /// Reformats the employment records in a per project structure.
        /// The entries are ordered in a way that allows more efficient search.
        /// </summary>
        /// <param name="records">The emloyment records as received in raw format</param>
        /// <returns>A project based structure</returns>
        public Dictionary<int, List<EmploymentDuration>> ReformatData(List<EmploymentRecord> records);

        public IEnumerable<EmploymentRecord> ReadRecords(string file);

    }
}
