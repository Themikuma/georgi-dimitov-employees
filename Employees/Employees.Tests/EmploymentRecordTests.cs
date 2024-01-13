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
    public class EmploymentRecordTests
    {
        [Fact]
        public void Test_CreateEmploymentRecord_WithTrimmedRecord()
        {
            string line = "143,12,2013-11-01,2014-01-05";
            IDataIngestionService service = new DataIngestionService();

            EmploymentRecord result = new EmploymentRecord(line);

            Assert.Equal(143, result.EmpId);
            Assert.Equal(12, result.ProjectId);
            Assert.Equal(new DateTime(2013, 11, 1), result.DateFrom);
            Assert.Equal(new DateTime(2014, 1, 5), result.DateTo);
        }

        [Fact]
        public void Test_CreateEmploymentRecord_WithSpacesInRecord()
        {
            string line = "143, 10, 2009-01-01    ,    2011-04-27";
            IDataIngestionService service = new DataIngestionService();

            EmploymentRecord result = new EmploymentRecord(line);

            Assert.Equal(143, result.EmpId);
            Assert.Equal(10, result.ProjectId);
            Assert.Equal(new DateTime(2009, 1, 1), result.DateFrom);
            Assert.Equal(new DateTime(2011, 4, 27), result.DateTo);
        }

        [Fact]
        public void Test_CreateEmploymentRecord_WithNullToDate()
        {
            string line = "143,12,2013-11-01, NULL";
            IDataIngestionService service = new DataIngestionService();

            EmploymentRecord result = new EmploymentRecord(line);

            Assert.Equal(143, result.EmpId);
            Assert.Equal(12, result.ProjectId);
            Assert.Equal(new DateTime(2013, 11, 1), result.DateFrom);
            Assert.Equal(DateTime.Now, result.DateTo,TimeSpan.FromHours(8));
        }

        [Fact]
        public void Test_CreateEmploymentRecord_WithNullFromDate()
        {
            string line = "143,12,NULL  ,  NULL";
            IDataIngestionService service = new DataIngestionService();

            EmploymentRecord result = new EmploymentRecord(line);

            Assert.Equal(143, result.EmpId);
            Assert.Equal(12, result.ProjectId);
            Assert.Equal(DateTime.Now, result.DateFrom, TimeSpan.FromHours(8));
            Assert.Equal(DateTime.Now, result.DateTo, TimeSpan.FromHours(8));
        }
    }
}
