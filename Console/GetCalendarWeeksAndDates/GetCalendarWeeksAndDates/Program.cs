using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCalendarWeeksAndDates
{
    class Program
    { 
        public static void Green()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void Yellow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static void Gray()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void Main(string[] args)
        {
            /*
            #region Current Date
            
            int cw;
            DateTime currDate = DateTime.Today;

            Console.WriteLine($"Date today: {currDate}");

            List<DateTime> dates = GetWeekDatesForDate(currDate);

            cw = GetCalendarWeek(currDate);

            Console.WriteLine($"Dates of the current week: \"{cw}\"");
            foreach (var item in dates)
            {
                Console.WriteLine($"Date: {item}");
            }

            #endregion
            */


            // 2019 January
            TestCWForYear(2019);

            // 2018 January
            TestCWForYear(2018);

            // 2017 January
            TestCWForYear(2017);

            // 2016 January
            TestCWForYear(2016);

            // 2015 January
            TestCWForYear(2015);

            // 2014 January
            TestCWForYear(2014);

            // 2013 January
            TestCWForYear(2013);

            // 2012 January
            TestCWForYear(2012);

            // 2011 January
            TestCWForYear(2011);

            // 2010 January
            TestCWForYear(2010);

            // 2009 January
            TestCWForYear(2009);

        }

        public static List<DateTime> GetWeekDatesForDate(DateTime date)
        {
            int currentDayOfWeek = (int)date.DayOfWeek;
            DateTime sunday = date.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);
            // If we started on Sunday, we should actually have gone *back*
            // 6 days instead of forward 1...
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            List<DateTime> dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
            return dates;
        }

        public static int GetCalendarWeek(DateTime date)
        {
            CultureInfo myCI = new CultureInfo("de-DE");
            Calendar myCal = myCI.Calendar;

            //DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            //Calendar cal = dfi.Calendar;

            int intKW = myCal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return intKW;
        }

        public static void GetOutput(DateTime date)
        {
            int cw;
            Console.WriteLine($"\n\n\tDate: {date}");

            List<DateTime> dates = GetWeekDatesForDate(date);

            cw = GetCalendarWeek(date);

            Console.WriteLine($"\tDates for the calendar-week: \"{cw}\"");
            foreach (var item in dates)
            {
                Console.WriteLine($"\tDate: {item.DayOfWeek}, {item.Day}.{item.Month}.{item.Year}");
            }
        }

        public static void TestCWForYear(int year)
        {
            Green();
            DateTime third = new DateTime(year, 01, 03);
            GetOutput(third);

            Gray();
            DateTime fourth = new DateTime(year, 01, 04);
            GetOutput(fourth);

            Yellow();
            DateTime fifth = new DateTime(year, 01, 05);
            GetOutput(fifth);


            Gray();
            Console.WriteLine($"\n\tThis was for year: \"{third.Year}\"....");
            Console.ReadKey();

            Console.WriteLine("\n\t##################################################################################\n");
        }
    }
}