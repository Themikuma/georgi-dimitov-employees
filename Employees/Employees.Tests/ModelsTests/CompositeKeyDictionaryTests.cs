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

            Assert.Equal(3, dictionary.GetDuration(duration1.EmpId, duration2.EmpId));
        }

        [Fact]
        public void Test_AddInterval_With_OneEmployee()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("143, 10, 2012-05-16, NULL"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);
            Assert.Throws<KeyNotFoundException>(() => dictionary.GetDuration(duration1.EmpId, duration2.EmpId));
        }

        [Fact]
        public void Test_AddInterval_With_PerfectlyOverlappingIntervals()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-01, 2013-01-03"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);

            Assert.Equal(3, dictionary.GetDuration(duration1.EmpId, duration2.EmpId));
        }

        [Fact]
        public void Test_AddInterval_With_SingleOverlappingDay()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-01, 2013-01-01"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2012-01-01, 2013-01-01"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);

            Assert.Equal(1, dictionary.GetDuration(duration1.EmpId, duration2.EmpId));
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
            dictionary.Add(duration4, duration3);

            Assert.Equal(5, dictionary.GetDuration(duration1.EmpId, duration2.EmpId));
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

        [Fact]
        public void Test_GetMaxDuration_With_MultipleOverlappingEmployees()
        {
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2012-05-16, NULL"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"));
            EmploymentDuration duration3 = new EmploymentDuration(new EmploymentRecord("143, 10, 2011-01-01, 2011-01-03"));
            EmploymentDuration duration4 = new EmploymentDuration(new EmploymentRecord("218, 10, 2010-05-16, 2012-03-16"));
            EmploymentDuration duration5 = new EmploymentDuration(new EmploymentRecord("1337, 10, 2011-01-01, 2011-01-07"));
            EmploymentDuration duration6 = new EmploymentDuration(new EmploymentRecord("1338, 10, 2010-05-16, 2012-03-16"));

            dictionary.Add(duration1, duration2);
            dictionary.Add(duration4, duration3);
            dictionary.Add(duration5, duration6);
            CommonEmployment expected = new CommonEmployment(1337, 1338, 7);
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
