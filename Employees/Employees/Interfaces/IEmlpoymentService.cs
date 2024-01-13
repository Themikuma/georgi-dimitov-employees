using Employees.Models;

namespace Employees.Interfaces
{
    public interface IEmlpoymentService
    {
        /// <summary>
        /// Calculates the longest time two employees have worked together on a project
        /// </summary>
        /// <returns></returns>
        Task<CommonEmployment> CalculateLongestCommonEmlpoyment();
    }
}