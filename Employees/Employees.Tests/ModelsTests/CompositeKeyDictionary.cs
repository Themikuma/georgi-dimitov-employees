using Employees.Models;

namespace Employees.Tests.ModelsTests
{
    internal class CompositeKeyDictionary
    {
        private readonly Dictionary<string, int> _calculatedDurations;
        public CompositeKeyDictionary()
        {
            _calculatedDurations = new Dictionary<string, int>();
        }

        internal void Add(EmploymentDuration duration1, EmploymentDuration duration2)
        {
            List<long> values =
            [
                duration1.DateTo - duration1.DateFrom,//endA - startA
                duration1.DateTo - duration2.DateFrom,//endA - startB
                duration2.DateTo - duration1.DateFrom,//endB - startA
                duration2.DateTo - duration2.DateFrom,//endB - startB
            ];
            long overlap = values.Min();

            if (overlap >= 0)
            {
                string key = CreateKey(duration1.EmpId, duration2.EmpId);
                if (_calculatedDurations.ContainsKey(key))
                {
                    _calculatedDurations[key] += (int)(overlap / Constants.SecondsInDay);
                }
                else
                {
                    _calculatedDurations.Add(key, (int)(overlap / Constants.SecondsInDay));
                }                
            }
        }

        internal string GetKey(EmploymentDuration duration1, EmploymentDuration duration2)
        {
            return CreateKey(duration1.EmpId, duration2.EmpId);
        }

        internal int GetDuration(int EmpId1, int EmpId2)
        {
            var key = CreateKey(EmpId1, EmpId2);
            return _calculatedDurations[key];
        }

        private string CreateKey(int employeeId1, int employeeId2)
        {
            string key;
            if (employeeId1 > employeeId2)
            {
                key = $"{employeeId2},{employeeId1}";
            }
            else
            {
                key = $"{employeeId1},{employeeId2}";
            }
            return key;
        }
    }
}