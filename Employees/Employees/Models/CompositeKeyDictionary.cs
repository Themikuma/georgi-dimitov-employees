
namespace Employees.Models
{
    /// <summary>
    /// Creates a composite key dictionary that calculates a unique composite key for 
    /// employees and adds up days together on a project as a value.
    /// </summary>
    public class CompositeKeyDictionary//TODO: Keep thinking of a better name for this. This name describes what it is instead of what it does.
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

        /// <summary>        
        /// Gets the duration two employees have spent working together on common projects
        /// </summary>
        /// <param name="empId1"></param>
        /// <param name="empId2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public CommonEmployment GetDuration(int empId1, int empId2)
        {
            var key = CreateKey(empId1, empId2);
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(Constants.SameEmployeeId);
            }
            return GetCommonEmployment(key);
        }

        /// <summary>
        /// Gets the pair of employees who have worked together on a project for the longest time.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when no overlapping periods have been found</exception>
        public CommonEmployment GetMaxDuration()
        {
            int max = 0;
            string maxKey = "";
            foreach (var key in _calculatedDurations.Keys)
            {
                if (_calculatedDurations[key] > max)
                {
                    max = _calculatedDurations[key];
                    maxKey = key;
                }
            }
            if (string.IsNullOrWhiteSpace(maxKey))
            {
                throw new ArgumentException(Constants.NoOverlappingEmployeesMessage);
            }
            return GetCommonEmployment(maxKey);
        }

        private CommonEmployment GetCommonEmployment(string maxKey)
        {
            var parts = maxKey.Split(',');
            return new CommonEmployment(int.Parse(parts[0]), int.Parse(parts[1]), _calculatedDurations[maxKey]);
        }

        private string CreateKey(int employeeId1, int employeeId2)
        {
            if (employeeId1 == employeeId2)
            {
                return string.Empty;
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