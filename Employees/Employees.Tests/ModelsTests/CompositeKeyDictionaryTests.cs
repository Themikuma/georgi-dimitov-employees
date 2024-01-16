using Employees.Models;

namespace Employees.Tests.ModelsTests
{
    public class CompositeKeyDictionaryTests
    {
        [Fact]
        public void Test_AddInterval_With_SingleOverlappingInterval()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2012-05-16, NULL"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);

            Assert.Equal(3, dictionary.GetMaxDuration().TimeInDays);
        }

        [Fact]
        public void Test_AddInterval_With_OneEmployee()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("143, 10, 2012-05-16, NULL"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);
            Assert.Multiple(() =>
            {
                var exception = Assert.Throws<ArgumentException>(() => dictionary.GetMaxDuration().TimeInDays);
                Assert.Equal(Constants.NoOverlappingEmployeesMessage, exception.Message);
            });
        }

        [Fact]
        public void Test_AddInterval_With_PerfectlyOverlappingIntervals()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-01, 2013-01-03"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);

            Assert.Equal(3, dictionary.GetMaxDuration().TimeInDays);
        }

        [Fact]
        public void Test_AddInterval_With_SingleOverlappingDay()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-01, 2013-01-01"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2012-01-01, 2013-01-01"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);

            Assert.Equal(1, dictionary.GetMaxDuration().TimeInDays);
        }

        [Fact]
        public void Test_AddInterval_With_MultipleOverlappingIntervals()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-02, NULL"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"));
            EmploymentDuration duration3 = new EmploymentDuration(new EmploymentRecord("143, 10, 2011-01-01, 2011-01-03"));
            EmploymentDuration duration4 = new EmploymentDuration(new EmploymentRecord("218, 10, 2010-05-16, 2012-03-16"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);
            dictionary.Add(duration1, duration3);
            dictionary.Add(duration1, duration4);
            dictionary.Add(duration2, duration3);
            dictionary.Add(duration2, duration4);
            dictionary.Add(duration4, duration3);

            Assert.Equal(5, dictionary.GetMaxDuration().TimeInDays);
        }

        [Fact]
        public void Test_AddInterval_With_MultipleOverlappingEmployees()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-01, NULL"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-04"));
            EmploymentDuration duration3 = new EmploymentDuration(new EmploymentRecord("1, 10, 2011-01-01, 2011-01-03"));
            EmploymentDuration duration4 = new EmploymentDuration(new EmploymentRecord("2, 10, 2010-05-16, 2012-03-16"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);
            dictionary.Add(duration1, duration3);
            dictionary.Add(duration1, duration4);
            dictionary.Add(duration2, duration3);
            dictionary.Add(duration2, duration4);
            dictionary.Add(duration4, duration3);
            var actual = dictionary.GetMaxDuration();
            Assert.Multiple(() =>
            {
                Assert.Equal(4, actual.TimeInDays);
                Assert.Equal(143, actual.FirstEmpId);
                Assert.Equal(218,actual.SecondEmpId);
            });
        }

        [Fact]
        public void Test_GetMaxDuration_With_SingleOverlappingEmployee()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-01, 2013-01-01"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-01"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);
            CommonEmployment expected = new CommonEmployment(143, 218, 1);
            CommonEmployment actual = dictionary.GetMaxDuration();
            Assert.Multiple(() =>
            {
                Assert.Equal(143, actual.FirstEmpId);
                Assert.Equal(218, actual.SecondEmpId);
                Assert.Equal(1, actual.TimeInDays);
            });
        }

        [Fact]
        public void Test_GetDuration_With_ExistingEmployeeIds()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-01, 2013-01-01"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-01"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);
            int actual = dictionary.GetDuration(duration1.EmpId, duration2.EmpId);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Test_GetDuration_With_RepeatingEmployeeId()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-01, 2013-01-01"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-01"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);
            var actual = Assert.Throws<ArgumentException>(() => dictionary.GetDuration(duration1.EmpId, duration1.EmpId));
            Assert.Equal(Constants.SameEmployeeId, actual.Message);
        }
    }
}
