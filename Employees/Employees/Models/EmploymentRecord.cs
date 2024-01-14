namespace Employees.Models
{
    public class EmploymentRecord
    {
        public EmploymentRecord(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length != 4)
            {
                throw new ArgumentException("Invalid line");
            }
            EmpId = int.Parse(parts[0]);
            ProjectId = int.Parse(parts[1]);
            var now = DateTime.Now;
            parts[2] = parts[2].Trim();
            parts[3] = parts[3].Trim();
            DateFrom = parts[2].Equals(Constants.EmptyDate, StringComparison.InvariantCultureIgnoreCase) ? now : DateTime.Parse(parts[2]);
            DateTo = parts[3].Equals(Constants.EmptyDate, StringComparison.InvariantCultureIgnoreCase) ? now : DateTime.Parse(parts[3]);
        }

        public int EmpId { get; }
        public int ProjectId { get; }
        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }
    }

}
