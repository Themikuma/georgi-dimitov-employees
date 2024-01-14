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
                var exception = Assert.Throws<ArgumentOutOfRangeException>(() => dictionary.GetMaxDuration().TimeInDays);
                Assert.Equal(Constants.NoOverlappingEmployeesMessage, exception.ParamName);
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
                Assert.Equal(expected.FirstEmpId, actual.FirstEmpId);
                Assert.Equal(expected.SecondEmpId, actual.SecondEmpId);
                Assert.Equal(expected.TimeInDays, actual.TimeInDays);
            });

        }
    }
}
