
namespace Employees.Models
{

    public class CompositeKeyDictionary
    {
        private readonly Dictionary<string, int> _calculatedDurations;
        public CompositeKeyDictionary()
        {
            _calculatedDurations = new Dictionary<string, int>();
        }

        /// <summary>
        /// Adds the duration in days using a composite key in the format "EmpId1, EmpId2"
        /// where EmpID1 is the smaller of the two
        /// </summary>
        /// <param name="duration1"></param>
        /// <param name="duration2"></param>
        public void Add(EmploymentDuration duration1, EmploymentDuration duration2)
        {
            string key = CreateKey(duration1.EmpId, duration2.EmpId);
            if (!string.IsNullOrWhiteSpace(key))
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
                    //Add a day in order to properly close the interval
                    overlap += Constants.SecondsInDay;
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
        }

        public int GetDuration(int EmpId1, int EmpId2)
        {
            var key = CreateKey(EmpId1, EmpId2);
            return _calculatedDurations[key];
        }

        public int GetDuration(string key) => _calculatedDurations[key];

        public CommonEmployment GetMaxDuration()
        {
            throw new NotImplementedException();
        }

        private string CreateKey(int employeeId1, int employeeId2)
        {
            if (employeeId1==employeeId2)
            {
                return "";
            }
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