namespace Employees.Models
{
    public class EmploymentDuration
    {
        public int EmpId { get; }
        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }

        public EmploymentDuration(int empId, DateTime dateFrom, DateTime dateTo)
        {
            EmpId = empId;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}