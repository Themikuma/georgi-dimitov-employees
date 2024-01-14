using Employees.Interfaces;
using Employees.Models;
using Employees.Services;

namespace Employees.Tests.ServicesTests
{
    public class DataIngestionServiceTests
    {
        [Fact]
        public void Test_ReformatData_With_MultipleEntries()
        {
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("143, 12, 2013-11-01, 2014-01-05"),
                new EmploymentRecord("143, 10, 2009-01-01, 2011-04-27"),
                new EmploymentRecord("218, 10, 2012-05-16, NULL")
            };
            IDataIngestionService service = new DataIngestionService();

            var result = service.ReformatData(records);
            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.NotNull(result[10]);
                Assert.NotNull(result[12]);
                Assert.Equal(218, result[10][0].EmpId);
                Assert.Equal(2, result.Keys.Count);
            });
        }

        [Fact]
        public void Test_ReformatData_With_MultipleEntriesInSingleProject()
        {
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("218, 10, 2012-05-16, 2014-01-05"),
                new EmploymentRecord("143, 10, 2009-01-01, 2011-04-27"),
                new EmploymentRecord("142, 10, 2013-11-01, NULL")
            };
            IDataIngestionService service = new DataIngestionService();

            var result = service.ReformatData(records);
            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.NotNull(result[10]);
                Assert.Equal(142, result[10][0].EmpId);
            });
        }

        [Fact]
        public void Test_ReformatData_With_SingleProjectId()
        {
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("218, 10, 2012-05-16, NULL"),
                new EmploymentRecord("143, 10, 2009-01-01, 2011-04-27")
            };
            IDataIngestionService service = new DataIngestionService();

            var result = service.ReformatData(records);
            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.NotNull(result[10]);
                Assert.Single(result.Keys);
            });
        }

        [Fact]
        public void Test_ReformatData_With_EmptyRecordList()
        {
            List<EmploymentRecord> records = new List<EmploymentRecord>();
            IDataIngestionService service = new DataIngestionService();

            Assert.Throws<ArgumentException>(() => service.ReformatData(records));
        }

        [Fact]
        public void Test_ReadRecords_With_InvalidRecord()
        {
            string line = $"143, 12, 2013-11-01, 2014-01-05\n218, 10, NULL\n143, 10, 2009-01-01, 2011-04-27";
            IDataIngestionService service = new DataIngestionService();

            Assert.Throws<ArgumentException>(() => service.ReadRecords(line));
        }

        [Theory]
        [InlineData("143, 12, 2013-11-01, 2014-01-05", 1)]
        [InlineData($"143, 12, 2013-11-01, 2014-01-05\n218, 10, 2012-05-16, NULL\n143, 10, 2009-01-01, 2011-04-27", 3)]
        public void Test_ReadRecords(string content, int expected)
        {
            IDataIngestionService service = new DataIngestionService();

            var records = service.ReadRecords(content);

            Assert.Equal(records.Count(), expected);
        }
    }
}
