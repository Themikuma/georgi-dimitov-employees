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
            _ = new List<EmploymentDuration>();

            foreach (EmploymentRecord record in records)
            {
                List<EmploymentDuration> durations;
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

            //Reorder the entries by DateTo descending so searching will be more efficient.
            foreach (var key in result.Keys)
            {
                var ordered= result[key].OrderByDescending(t => t.DateTo).ToList();
                result[key]= ordered;
            }

            return result;
        }
    }
}
