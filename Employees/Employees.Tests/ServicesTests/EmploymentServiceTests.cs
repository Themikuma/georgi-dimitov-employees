using Employees.Interfaces;
using Employees.Models;
using Employees.Services;

namespace Employees.Tests.ServicesTests
{
    public class EmploymentServiceTests
    {
        [Fact]
        public void Test_CalculateLongestEmloyment_With_OverlappingEmployees()
        {
            IEmlpoymentService employmentService = new EmploymentService(new DataIngestionService());
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("143, 12, 2013-11-01, 2014-01-05"),
                new EmploymentRecord("218, 10, 2012-05-16, NULL"),
                new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03")
            };
            var result = employmentService.CalculateLongestCommonEmlpoyment(records);
            Assert.Equal(143, result.FirstEmpId);
            Assert.Equal(218, result.SecondEmpId);
            Assert.Equal(3, result.TimeInDays);
        }

        [Fact]
        public void Test_CalculateLongestEmloyment_With_NoOverlappingEmployees()
        {
            IEmlpoymentService employmentService = new EmploymentService(new DataIngestionService());
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("143, 12, 2013-11-01, 2014-01-05"),
                new EmploymentRecord("218, 10, 2012-05-16, NULL"),
                new EmploymentRecord("143, 10, 2011-01-01, 2011-01-03")
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => employmentService.CalculateLongestCommonEmlpoyment(records));
        }
    }
}