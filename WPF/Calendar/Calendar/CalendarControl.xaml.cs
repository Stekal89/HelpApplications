using Calendar.ControlModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calendar
{
    /// <summary>
    /// Interaction logic for CalendarControl.xaml
    /// </summary>
    public partial class CalendarControl : UserControl
    {
        MonthControl monthControl = null;
        WeekControl weekControl = null;
        DateTime currentDate = DateTime.Now;
        public CalendarControl()
        {
            InitializeComponent();
            cbMWD.SelectedItem = cbiMonth;
            //monthControl.LoadControls(grdMonth);
            List<DateTime> datesOfMonth = GetAllDaysOfMonth(currentDate.Year, currentDate.Month);
        }

        /// <summary>
        /// Gets all dates of a month and returns it as list.
        /// </summary>
        /// <param name="year">year as Integer</param>
        /// <param name="month">month as Integer</param>
        /// <returns>List of days in one month</returns>
        private List<DateTime> GetAllDaysOfMonth(int year, int month)
        {
            List<DateTime> dates = new List<DateTime>();
            try
            {
                // Loop from the first day of the month until we hit the next month, moving forward a day at a time
                for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
                {
                    dates.Add(date);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nGetAllDaysOfMonth()".ToUpper());
                Debug.Indent();
                Debug.WriteLine("Error during loading, or adding dates of selected months!");
                Debug.WriteLine(ex);
                Debug.Unindent();
                Debugger.Break();
            }

            return dates;
        }

        /// <summary>
        /// Checks the first day of the month and sets the days for of month for the Month Grid starting with the first day.
        /// e.g. The first day is a saturday, so the grid will start with saturday.
        /// </summary>
        /// <param name="date">date which should be verified</param>
        /// <returns>true/false</returns>
        private bool ConfigureMonthControl(DateTime date)
        {
            try
            {
                List<DateTime> datesOfMonth = GetAllDaysOfMonth(date.Year, date.Month);

                switch (datesOfMonth[0].DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        monthControl.ConfigureDayControls(0, datesOfMonth, mGridLastRow);
                        break;
                    case DayOfWeek.Tuesday:
                        monthControl.ConfigureDayControls(1, datesOfMonth, mGridLastRow);
                        break;
                    case DayOfWeek.Wednesday:
                        monthControl.ConfigureDayControls(2, datesOfMonth, mGridLastRow);
                        break;
                    case DayOfWeek.Thursday:
                        monthControl.ConfigureDayControls(3, datesOfMonth, mGridLastRow);
                        break;
                    case DayOfWeek.Friday:
                        monthControl.ConfigureDayControls(4, datesOfMonth, mGridLastRow);
                        break;
                    case DayOfWeek.Saturday:
                        monthControl.ConfigureDayControls(5, datesOfMonth, mGridLastRow);
                        break;
                    case DayOfWeek.Sunday:
                        monthControl.ConfigureDayControls(6, datesOfMonth, mGridLastRow);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nInitializeMonthControl()".ToUpper());
                Debug.Indent();
                Debug.WriteLine("Error during initialization of MonthGrid...");
                Debug.WriteLine(ex);
                Debug.Unindent();
                Debugger.Break();
                return false;
            }

            Debug.Unindent();

            return true;
        }

        /// <summary>
        /// Handling during clicking on the Next/Previos buttons.
        /// </summary>
        /// <param name="sender">MonthControl/WeekControl/DayControl</param>
        /// <param name="e"></param>
        private void ClickNextPreviousMonth(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int i = 0;

            if (cbMWD.SelectedItem == cbiMonth)
            {
                if (btn.Name == "btnPrevious")
                    i--;
                else if (btn.Name == "btnNext")
                    i++;

                monthControl.DeselectDayControl(currentDate);
                currentDate = currentDate.AddMonths(i);
                currentDate = new DateTime(currentDate.Year, currentDate.Month, 1);
                lblYear.Content = currentDate.Year.ToString();
                lblHeader.Content = currentDate.ToString("MMMM");
                ConfigureMonthControl(currentDate);
            }
            else if (cbMWD.SelectedItem == cbiWeek)
            {
                //if (btn.Name == "btnPrevious")
                //    i -= 7;
                //else if (btn.Name == "btnNext")
                //    i += 7;

                //currentDate = currentDate.AddDays(i);

                //// Set date to the Monday!!!

                //if (weekControl == null)
                //{
                //    weekControl = new WeekControl();
                //    weekControl.LoadControls(grdWeek);
                //}

                //lblYear.Content = currentDate.Year.ToString();
                //lblHeader.Content = $"KW: {weekControl.GetCalendarWeek(currentDate)}";

                //List<DateTime> weekDates = weekControl.GetWeekDatesForDate(currentDate);

                //ConfigureWeekControl(currentDate);
                //DateTime actDay = new DateTime();
                //WeekControl.WeekDay deselectedWeekDay = weekControl.Days.Where(x => x.Day.IsSelected).FirstOrDefault();
                //if (deselectedWeekDay.Day != null)
                //    actDay = weekControl.DeselectDayControl(currentDate);

                //DateTime dayToSelect = weekDates.Where(x => x.DayOfWeek == DayOfWeek.Monday).FirstOrDefault();
                //currentDate = new DateTime(dayToSelect.Year, dayToSelect.Month, dayToSelect.Day);
            }
            else if (cbMWD.SelectedItem == cbiDay)
            {
            //    if (btn.Name == "btnPrevious")
            //        i--;
            //    else if (btn.Name == "btnNext")
            //        i++;

            //    currentDate = currentDate.AddDays(i);
            //    lblYear.Content = currentDate.Year.ToString();
            //    lblHeader.Content = currentDate.ToString("dd. MMMM");

            //    LoadMeetingsToDayControl();
            }
        }

        private void CbMWD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMWD.SelectedItem == cbiMonth)
            {
                grdMonth.Visibility = Visibility.Visible;
                grdWeek.Visibility = Visibility.Hidden;
                grdDay.Visibility = Visibility.Hidden;

                // Configure MonthControl
                lblYear.Content = currentDate.Year;
                lblHeader.Content = currentDate.ToString("MMMM");
                if (monthControl == null)
                {
                    monthControl = new MonthControl();
                    monthControl.LoadControls(grdMonth);
                }
                ConfigureMonthControl(currentDate);

                DateTime actDay = new DateTime();
                DayControl deselectedMonthDay = monthControl.DayControls.Where(x => x.IsSelected).FirstOrDefault();
                if (deselectedMonthDay != null)
                    actDay = monthControl.SelectDayControl(deselectedMonthDay.Date, currentDate);
                else
                    actDay = monthControl.SelectDayControl(new DateTime(), currentDate);

                deselectedMonthDay = monthControl.DayControls.Where(x => x.IsSelected).FirstOrDefault();
            }
            else if (cbMWD.SelectedItem == cbiWeek)
            {
                grdMonth.Visibility = Visibility.Hidden;
                grdWeek.Visibility = Visibility.Visible;
                grdDay.Visibility = Visibility.Hidden;

                // Initialize
                if (weekControl == null)
                {
                    weekControl = new WeekControl();
                    weekControl.LoadControls(grdWeek);
                    //weekControl.LoadControls();
                }

                lblYear.Content = currentDate.Year.ToString();
                lblHeader.Content = $"KW: {weekControl.GetCalendarWeek(currentDate)}";

                //List<DateTime> l = weekControl.GetWeekDatesForDate(currentDate);
                weekControl.DatesOfWeek = weekControl.GetWeekDatesForDate(currentDate);


                weekControl.ConfigureWeekControl(currentDate);
                //DateTime actDay = new DateTime();
                //WeekControl.WeekDay deselectedWeekDay = weekControl.Days.Where(x => x.Day.IsSelected).FirstOrDefault();
                //if (deselectedWeekDay.Day != null)
                //    actDay = weekControl.SelectDayControl(deselectedWeekDay.Day.Date, currentDate);
                //else
                //    actDay = weekControl.SelectDayControl(new DateTime(), currentDate);

                //deselectedWeekDay = weekControl.Days.Where(x => x.Day.IsSelected).FirstOrDefault();
            }
            else if (cbMWD.SelectedItem == cbiDay)
            {
                grdMonth.Visibility = Visibility.Hidden;
                grdWeek.Visibility = Visibility.Hidden;
                grdDay.Visibility = Visibility.Visible;
            }

        }
    }
}
