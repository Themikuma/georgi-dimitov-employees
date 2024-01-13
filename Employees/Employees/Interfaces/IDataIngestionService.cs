﻿using Employees.Models;

namespace Employees.Interfaces
{
    public interface IDataIngestionService
    {
        /// <summary>
        /// Reformats the employment records in a per project structure that will be used to search through them in a more efficient manner.
        /// </summary>
        /// <param name="records">The emloyment records as received in raw format</param>
        /// <returns>A project based structure</returns>
        public Dictionary<int, List<EmploymentDuration>> ReformatData(List<EmploymentRecord> records);
     
    }
}