namespace Employees.Models
{
    public class ProjectCommonEmployment : CommonEmployment
    {
        public int ProjectId { get; }
        public ProjectCommonEmployment(int firstEmpId, int secondEmpId, int timeInDays, int projectId) : base(firstEmpId, secondEmpId, timeInDays)
        {
            ProjectId = projectId;
        }
    }
}
