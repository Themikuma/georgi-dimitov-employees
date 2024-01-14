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
            Dictionary<int, List<EmploymentDuration>> reformatedData = _ingestionService.ReformatData(records.ToList());

            foreach (int key in reformatedData.Keys)
            {
                var durations = reformatedData[key];
                for (int i = 0; i < durations.Count; i++)
                {
                    for (int j = i + 1; j < durations.Count; j++)
                    {
                        if (durations[i].DateFrom > durations[j].DateTo)
                        {
                            break; //Because we've ordered the data by DateTo we can afford this optimizaton.
                        }
                        if (durations[i].DateFrom <= durations[j].DateTo && durations[i].DateFrom >= durations[j].DateTo)
                        {
                            //Do calculations
                        }
                    }
                }
            }
            throw new NotImplementedException();
        }
    }
}
