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

            Assert.Multiple(() =>
            {
                Assert.Equal(3, dictionary.GetDuration(duration1.EmpId, duration2.EmpId));
                Assert.Equal("143,218", dictionary.GetKey(duration2, duration1));
            });
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

            Assert.Multiple(() =>
            {
                Assert.Equal(3, dictionary.GetDuration(duration1.EmpId, duration2.EmpId));
                Assert.Equal("143,218", dictionary.GetKey(duration2, duration1));
            });
        }

        [Fact]
        public void Test_AddInterval_With_SingleOverlappingDay()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2013-01-01, 2013-01-01"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-01"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);

            Assert.Multiple(() =>
            {
                Assert.Equal(1, dictionary.GetDuration(duration1.EmpId, duration2.EmpId));
                Assert.Equal("143,218", dictionary.GetKey(duration2, duration1));
            });
        }

        [Fact]
        public void Test_AddInterval_With_MultipleOverlappingIntervals()
        {
            EmploymentDuration duration1 = new EmploymentDuration(new EmploymentRecord("218, 10, 2012-05-16, NULL"));
            EmploymentDuration duration2 = new EmploymentDuration(new EmploymentRecord("143, 10, 2013-01-01, 2013-01-03"));
            EmploymentDuration duration3 = new EmploymentDuration(new EmploymentRecord("143, 10, 2011-01-01, 2011-01-03"));
            EmploymentDuration duration4 = new EmploymentDuration(new EmploymentRecord("218, 10, 2010-05-16, 2012-03-16"));
            CompositeKeyDictionary dictionary = new CompositeKeyDictionary();

            dictionary.Add(duration1, duration2);
            dictionary.Add(duration4, duration3);

            Assert.Multiple(() =>
            {
                Assert.Equal(6, dictionary.GetDuration(duration1.EmpId, duration2.EmpId));
                Assert.Equal("143,218", dictionary.GetKey(duration1, duration2));//The order of durations shouldn't matter. Key should always have lower EmpId
            });
        }
    }
}
