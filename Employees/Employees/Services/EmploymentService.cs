using Employees.Interfaces;
using Employees.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Collections.Generic;

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
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

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
                        if (durations[i].DateFrom <= durations[j].DateTo && durations[i].DateTo >= durations[j].DateFrom)
                        {
                            dictionary.Add(durations[i], durations[j]);
                        }
                    }
                }
            }
            return dictionary.GetMaxDuration();
        }

        public IEnumerable<ProjectCommonEmployment> GetProjectCommonEmployments(IEnumerable<EmploymentRecord> records)
        {
            var bestPair = CalculateLongestCommonEmlpoyment(records);
            int firstId = bestPair.FirstEmpId;
            int secondId = bestPair.SecondEmpId;
            List<ProjectCommonEmployment> result = new List<ProjectCommonEmployment>();
            Dictionary<int, List<EmploymentDuration>> reformatedData = _ingestionService.ReformatData(records.ToList());
            try
            {
                var ordered = reformatedData.OrderBy(t => t.Key);
                foreach (var item in ordered)
                {
                    CompositeKeyDictionary dictionary = new CompositeKeyDictionary();
                    var durations = reformatedData[item.Key];
                    for (int i = 0; i < durations.Count; i++)
                    {
                        for (int j = i + 1; j < durations.Count; j++)
                        {
                            if (durations[i].DateFrom > durations[j].DateTo)
                            {
                                break;
                            }
                            if (durations[i].DateFrom <= durations[j].DateTo && durations[i].DateTo >= durations[j].DateFrom)
                            {
                                dictionary.Add(durations[i], durations[j]);
                            }
                        }
                    }
                    result.Add(new ProjectCommonEmployment(firstId, secondId, dictionary.GetDuration(firstId, secondId), item.Key));
                }
            }
            catch (KeyNotFoundException)
            {
                //This can safely be supressed. Redesign with proper TryGet method later.
            }
            return result;
        }
    }
}
