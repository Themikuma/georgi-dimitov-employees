using Employees.Interfaces;
using Employees.Models;

namespace Employees.Services
{
    public class EmlpoymentService : IEmlpoymentService
    {
        public CommonEmployment CalculateLongestCommonEmlpoyment()
        {
            return new CommonEmployment(1, 1, 1);
        }
    }
}
