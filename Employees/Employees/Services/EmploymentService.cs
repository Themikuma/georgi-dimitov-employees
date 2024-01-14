using Employees.Interfaces;
using Employees.Models;

namespace Employees.Services
{
    public class EmploymentService : IEmlpoymentService
    {
        private IDataIngestionService _ingestionService;

        public EmploymentService(IDataIngestionService ingestionService)
        {
            _ingestionService = ingestionService;
        }

        public CommonEmployment CalculateLongestCommonEmlpoyment(IEnumerable<EmploymentRecord> records)
        {
            var reformatedData=_ingestionService.ReformatData(records.ToList());
            throw new NotImplementedException();
        }
    }
}
