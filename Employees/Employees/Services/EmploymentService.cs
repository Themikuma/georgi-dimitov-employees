using Employees.Interfaces;
using Employees.Models;

namespace Employees.Services
{
    public class EmploymentService : IEmlpoymentService
    {
        public async Task<CommonEmployment> CalculateLongestCommonEmlpoyment()
        {
            return new CommonEmployment(1, 1, 1);
        }
    }
}
