using Employees.Interfaces;
using Employees.Models;

namespace Employees.Services
{
    public class DataIngestionService : IDataIngestionService
    {
        public Dictionary<int, List<EmploymentDuration>> ReformatData(List<EmploymentRecord> records)
        {
            if (records.Count == 0)
            {
                throw new ArgumentException();
            }
            Dictionary<int, List<EmploymentDuration>> result = new Dictionary<int, List<EmploymentDuration>>();
            List<EmploymentDuration> durations = new List<EmploymentDuration>();

            foreach (EmploymentRecord record in records)
            {
                if (result.TryGetValue(record.ProjectId, out durations))
                {
                    durations.Add(new EmploymentDuration(record));
                }
                else
                {
                    result.Add(record.ProjectId, new List<EmploymentDuration>());
                    result[record.ProjectId].Add(new EmploymentDuration(record));
                }
            }

            return result;
        }
    }
}
