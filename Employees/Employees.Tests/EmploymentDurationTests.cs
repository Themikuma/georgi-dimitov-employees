using Employees.Models;

namespace Employees.Tests
{
    public class EmploymentDurationTests
    {

        [Fact]
        public void Test_CreateEmploymentDuration_From_EmploymentRecord()
        {
            string line = "143,12,2023-01-10,2023-01-13";
            EmploymentRecord record = new EmploymentRecord(line);

            EmploymentDuration result = new EmploymentDuration(record);
            Assert.Multiple(() =>
            {
                Assert.Equal(record.EmpId, result.EmpId);
                Assert.Equal(1673301600, result.DateFrom);
                Assert.Equal(1673560800, result.DateTo);
            });
        }
    }
}
