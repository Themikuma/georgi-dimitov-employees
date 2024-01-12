namespace Employees.Models
{
    public class CommonEmployment
    {
        public int FirstEmpId { get; }
        public int SecondEmpId { get; }
        public int TimeInDays { get; }

        public CommonEmployment(int firstEmpId, int secondEmpId, int timeInDays)
        {
            FirstEmpId = firstEmpId;
            SecondEmpId = secondEmpId;
            TimeInDays = timeInDays;
        }
    }
}