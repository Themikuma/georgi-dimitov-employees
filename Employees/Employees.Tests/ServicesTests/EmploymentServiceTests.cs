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
            Assert.Multiple(() =>
            {
                Assert.Equal(143, result.FirstEmpId);
                Assert.Equal(218, result.SecondEmpId);
                Assert.Equal(3, result.TimeInDays);
            });
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
            Assert.Throws<ArgumentException>(() => employmentService.CalculateLongestCommonEmlpoyment(records));
        }


        [Fact]
        public void Test_CalculateLongestCommonEmlpoyment_With_MultipleOverlappingEmployees()
        {
            IEmlpoymentService employmentService = new EmploymentService(new DataIngestionService());
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("218, 10, 2012-05-16, NULL"),
                new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"),
                new EmploymentRecord("143, 10, 2011-01-01, 2011-01-03"),
                new EmploymentRecord("218, 10, 2010-05-16, 2012-03-16"),
                new EmploymentRecord("1337, 10, 2011-01-01, 2011-01-07"),
                new EmploymentRecord("1338, 10, 2010-05-16, 2012-03-16")
            };
            var result = employmentService.CalculateLongestCommonEmlpoyment(records);
            Assert.Multiple(() =>
            {
                Assert.Equal(218, result.FirstEmpId);
                Assert.Equal(1338, result.SecondEmpId);
                Assert.Equal(671, result.TimeInDays);
            });
        }

        [Fact]
        public void Test_CalculateLongestCommonEmlpoyment_With_ThreeWayTie()
        {
            IEmlpoymentService employmentService = new EmploymentService(new DataIngestionService());
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("218, 10, 2012-05-16, NULL"),
                new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"),
                new EmploymentRecord("143, 10, 2011-01-01, 2011-01-03"),
                new EmploymentRecord("218, 10, 2010-05-16, 2012-03-16"),
                new EmploymentRecord("1337, 10, 2011-01-01, 2011-01-06")
            };
            var result = employmentService.CalculateLongestCommonEmlpoyment(records);
            Assert.Multiple(() =>
            {
                Assert.Equal(143, result.FirstEmpId);
                Assert.Equal(218, result.SecondEmpId);
                Assert.Equal(6, result.TimeInDays);
            });
        }

        /// <summary>
        /// The list checked here isn't complete. For a proper solution the CSV should probably provide region data.
        /// Multiple common data formats are supported.
        /// </summary>
        [Fact]
        public void Test_CalculateLongestEmloyment_With_DifferentDateFormats()
        {
            IEmlpoymentService employmentService = new EmploymentService(new DataIngestionService());
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("143, 12, 2013-11-01,05-Jan-2014"),//yy-MM-dd, dd-MMMM-yyyy
                new EmploymentRecord("218, 10,5-16-12, NULL"),//M-d-yy
                new EmploymentRecord("143, 10, 2013-06-21, 2013/6/23"),//M/d/yy yyyy/M/d 
                new EmploymentRecord("1, 10, 12.01.24, 12/02/24"),//MM.dd.yy MM/dd/yy
            };
            var result = employmentService.CalculateLongestCommonEmlpoyment(records);
            Assert.Multiple(() =>
            {
                Assert.Equal(143, result.FirstEmpId);
                Assert.Equal(218, result.SecondEmpId);
                Assert.Equal(3, result.TimeInDays);
            });
        }

        [Fact]
        public void Test_GetProjectCommonEmployments_With_MultipleOverlappingEmployees()
        {
            IEmlpoymentService employmentService = new EmploymentService(new DataIngestionService());
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("218, 10, 2012-05-16, NULL"),
                new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"),
                new EmploymentRecord("143, 10, 2011-01-01, 2011-01-03"),
                new EmploymentRecord("218, 10, 2010-05-16, 2012-03-16"),
                new EmploymentRecord("1337, 10, 2011-01-01, 2011-01-07"),
                new EmploymentRecord("1338, 10, 2010-05-16, 2012-03-16"),
                new EmploymentRecord("218, 1, 2011-01-01, 2011-01-07"),
                new EmploymentRecord("143, 1, 2010-05-16, 2012-03-16")
            };
            List<ProjectCommonEmployment> result = employmentService.GetProjectCommonEmployments(records).ToList();
            Assert.Multiple(() =>
            {
                Assert.Equal(143, result[0].FirstEmpId);
                Assert.Equal(218, result[0].SecondEmpId);
                Assert.Equal(1, result[0].ProjectId);
                Assert.Equal(7, result[0].TimeInDays);
                Assert.Equal(2, result.Count);
            });
        }
    }
}