using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Tests
{
    public class EmploymentDurationTests
    {

        [Fact]
        public void Test_CreateEmploymentDuration_From_EmploymentRecord()
        {
            string line = "143,12,2023-01-10,2023-01-13";
            EmploymentRecord record = new EmploymentRecord(line);

            EmploymentDuration result=new EmploymentDuration(record);
            Assert.Equal(record.EmpId, result.EmpId);
            Assert.Equal(1673301600, result.DateFrom);
            Assert.Equal(1673560800, result.DateTo);
        }
    }
}
