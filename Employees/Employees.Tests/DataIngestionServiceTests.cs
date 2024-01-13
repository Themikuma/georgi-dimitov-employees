using Employees.Interfaces;
using Employees.Models;
using Employees.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Tests
{
    public class DataIngestionServiceTests
    {
        [Fact]
        public void Test_ReformatData_With_MultipleEntries()
        {
            List<EmploymentRecord> records = new List<EmploymentRecord>()
            {
                new EmploymentRecord("143, 12, 2013-11-01, 2014-01-05"),
                new EmploymentRecord("218, 10, 2012-05-16, NULL"),
                new EmploymentRecord("143, 10, 2009-01-01, 2011-04-27")
            };
            IDataIngestionService service = new DataIngestionService();

            var result = service.ReformatData(records);
            Assert.NotNull(result);
            Assert.NotNull(result[10]);
            Assert.NotNull(result[12]);
            Assert.Equal(2, result.Keys.Count);
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
            Assert.NotNull(result);
            Assert.NotNull(result[10]);
            Assert.Equal(2, result.Keys.Count);
        }

        [Fact]
        public void Test_ReformatData_With_EmptyRecordList()
        {
            List<EmploymentRecord> records = new List<EmploymentRecord>();
            IDataIngestionService service = new DataIngestionService();

           Assert.Throws<ArgumentException>(()=> service.ReformatData(records));
        }
    }
}
