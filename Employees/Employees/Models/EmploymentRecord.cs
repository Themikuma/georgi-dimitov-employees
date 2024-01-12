namespace Employees.Models
{
    public class EmploymentRecord
    {
        public int EmpId { get; }
        public int ProjectId { get; }
        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }

        public EmploymentRecord(int empId, int projectId, DateTime dateFrom, DateTime dateTo)
        {
            EmpId = empId;
            ProjectId = projectId;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }

}
