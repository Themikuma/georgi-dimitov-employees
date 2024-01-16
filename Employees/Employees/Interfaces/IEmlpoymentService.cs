using Employees.Models;

namespace Employees.Interfaces
{
    public interface IEmlpoymentService
    {
        /// <summary>
        /// Calculates the longest time two employees have worked together on a project
        /// </summary>
        /// <param name="records">The list of employees for which to calculate</param>
        /// <returns>A <see cref="CommonEmployment"/> containing the pair of employees and their days worked</returns>
        CommonEmployment CalculateLongestCommonEmlpoyment(IEnumerable<EmploymentRecord> records);
        
        /// <summary>
        /// Calculates a list of all common employments for the pair of employees that have spent the longest working together.
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        IEnumerable<ProjectCommonEmployment> GetProjectCommonEmployments(IEnumerable<EmploymentRecord> records);
    }
}