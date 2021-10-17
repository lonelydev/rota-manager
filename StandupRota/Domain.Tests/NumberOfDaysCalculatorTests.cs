using NUnit.Framework;
using System;

namespace Domain.Tests
{
    public class NumberOfDaysCalculatorTests
    {
        private INumberOfDaysCalculator _numberOfDaysCalculator;

        [SetUp]
        public void Setup()
        {
            _numberOfDaysCalculator = new NumberOfDaysCalculator();
        }

        [TestCaseSource(nameof(_weekDaysBetweenTestData))]
        public int TestWeekDaysBetween(DateTime startDate, DateTime endDate)
        {
            return _numberOfDaysCalculator.NumberOfWeekDaysBetween(startDate, endDate);
        }

        static object[] _weekDaysBetweenTestData =
        {
            new TestCaseData(new DateTime(2021,10,15), new DateTime(2021, 10, 17)).Returns(0),
            new TestCaseData(new DateTime(2021,10,15, 20,21,22), new DateTime(2021, 10, 17, 15,14,13)).Returns(0),
            new TestCaseData(new DateTime(2021,10,15, 20,21,22), new DateTime(2021, 10, 17, 22,14,13)).Returns(0),
            new TestCaseData(new DateTime(2021,10,15), new DateTime(2021, 10, 18)).Returns(1),
            new TestCaseData(new DateTime(2021,10,11), new DateTime(2021, 10, 18)).Returns(5),
            new TestCaseData(new DateTime(2021,10,11), new DateTime(2021, 10, 19)).Returns(6),
            new TestCaseData(new DateTime(2021,10,11), new DateTime(2021, 10, 11)).Returns(0),
        };
    }
}
