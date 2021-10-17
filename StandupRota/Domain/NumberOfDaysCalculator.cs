using System;

namespace Domain
{
    public interface INumberOfDaysCalculator
    {
        int NumberOfWeekDaysBetween(DateTime start, DateTime end);
    }
    public class NumberOfDaysCalculator : INumberOfDaysCalculator
    {
        public int NumberOfWeekDaysBetween(DateTime startDate, DateTime endDate)
        {
            var numDays = Math.Abs((startDate.Date - endDate.Date).Days);
            var numHolidays = NumberOfHolidaysSinceDate(startDate, numDays);
            return (numDays - numHolidays);
        }
        /// <summary>
        /// https://stackoverflow.com/a/43542119/2262959
        /// Currently, the big assumption is that Saturdays and Sundays are the only possible holidays in this system
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="numberOfDays"></param>
        /// <returns></returns>
        private static int NumberOfHolidaysSinceDate(DateTime startDate, int numberOfDays)
        {
            switch (startDate.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        return (numberOfDays / 7) * 2 + ((numberOfDays % 7 > 4) ? Math.Min(numberOfDays % 7 - 4, 2) : 0);
                    }
                case DayOfWeek.Tuesday:
                    {
                        return (numberOfDays / 7) * 2 + ((numberOfDays % 7 > 3) ? Math.Min(numberOfDays % 7 - 3, 2) : 0);
                    }
                case DayOfWeek.Wednesday:
                    {
                        return (numberOfDays / 7) * 2 + ((numberOfDays % 7 > 2) ? Math.Min(numberOfDays % 7 - 2, 2) : 0);
                    }
                case DayOfWeek.Thursday:
                    {
                        return (numberOfDays / 7) * 2 + ((numberOfDays % 7 > 1) ? Math.Min(numberOfDays % 7 - 1, 2) : 0);
                    }
                case DayOfWeek.Friday:
                    {
                        return (numberOfDays / 7) * 2 + ((numberOfDays % 7 > 0) ? Math.Min(numberOfDays % 7, 2) : 0);
                    }
                case DayOfWeek.Saturday:
                    {
                        return 1 + (numberOfDays / 7) * 2 + ((numberOfDays % 7 > 0) ? 1 : 0);
                    }
                case DayOfWeek.Sunday:
                    {
                        return 1 + (numberOfDays / 7) * 2 + ((numberOfDays % 7 > 5) ? 1 : 0);
                    }
            }
            return numberOfDays;
        }
    }
}
