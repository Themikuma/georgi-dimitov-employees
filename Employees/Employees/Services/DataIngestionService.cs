using Employees.Interfaces;
using Employees.Models;

namespace Employees.Services
{
    public class DataIngestionService : IDataIngestionService
    {
        public Dictionary<int, List<EmploymentDuration>> ReformatData(List<EmploymentRecord> records)
        {
            throw new NotImplementedException();
        }
    }
}
