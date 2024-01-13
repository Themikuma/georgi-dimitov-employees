namespace Employees.Models
{
    public class EmploymentDuration
    {
        public int EmpId { get; }
        public long DateFrom { get; }
        public long DateTo { get; }

        public EmploymentDuration(EmploymentRecord record)
        {
            EmpId = record.EmpId;
            DateFrom = ((DateTimeOffset) record.DateFrom).ToUnixTimeSeconds();
            DateTo = ((DateTimeOffset)record.DateTo).ToUnixTimeSeconds();
        }
    }
}