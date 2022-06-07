using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Calendar.ControlModel
{
    public class WeekControl
    {
        #region Properties

        public List<DayControl> DayControls { get; set; }

        public List<DateTime> DatesOfWeek { get; set; }

        #endregion

        #region Functions
        public bool LoadControls(Grid weekGrid)
        {
            DayControls = new List<DayControl>();

            Debug.Indent();
            try
            {
                foreach (var c in weekGrid.Children)
                {
                    DayControl dc = null;
                    dc = c as DayControl;

                    if (null != dc)
                    {
                        // Set here the Style of the label (Sunday also)
                        if (null != dc && !(dc.Name.StartsWith("wSon")))
                        {
                            dc.StyleWeekDayHeader();
                        }
                        else
                        {
                            dc.StyleWeekDayHeaderSunday();
                        }
                    }
                }
            }
                catch (Exception ex)
                {
                Debug.WriteLine("\nWeekControl.LoadControls()".ToUpper());
                Debug.Indent();
                Debug.WriteLine("Error during loading WeekControls:");
                Debug.WriteLine(ex);
                Debug.Unindent();
                Debugger.Break();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the Calendar-Week of a specific date.
        /// </summary>
        /// <param name="date">Date where you want to know the Calendar-Week</param>
        /// <returns>Calendar-Week as integer</returns>
        public int GetCalendarWeek(DateTime date)
        {
            try
            {
                System.Globalization.CultureInfo cI = new System.Globalization.CultureInfo("de-DE");
                System.Globalization.Calendar cal = cI.Calendar;

                int intKW = cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                return intKW;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nWeekControl.GetCalendarWeek()".ToUpper());
                Debug.Indent();
                Debug.WriteLine("Error during calculating \"Calendar-Week\"...");
                Debug.WriteLine(ex);
                Debug.Unindent();
                Debugger.Break();
                return -1;
            }
        }

        public List<DateTime> GetWeekDatesForDate(DateTime date)
        {
            int currentDayOfWeek = (int)date.DayOfWeek;
            DateTime sunday = date.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);

            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            List<DateTime> dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
            return dates;
        }

        public bool ConfigureWeekControl(DateTime date)
        {
            try
            {
                List<DateTime> dateList = GetWeekDatesForDate(date);

                // ##################################
                // Continue Here!!!
                // ##################################
                if (dateList.Count == DayControls.Count)
                {
                    for (int i = 0; i < dateList.Count; i++)
                    {
                        DayControls[i].lblDay.Content = $"{dateList[i].Day}.{dateList[i].ToString("MMM")}";
                        DayControls[i].Date = dateList[i];
                    }
                }
                else
                {
                    Debug.WriteLine("ConfigureWeekControl".ToUpper());
                    Debug.Indent();
                    Debug.WriteLine("Count of \"days of week\" does not match count of\"Week-Controls\"");
                    Debug.Unindent();
                    Debugger.Break();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ConfigureWeekControl".ToUpper());
                Debug.Indent();
                Debug.WriteLine("Error during configuring of WeekControls:");
                Debug.WriteLine(ex);
                Debug.Unindent();
                Debugger.Break();
                return false;
            }

            return false;
        }

        #endregion

    }
}
