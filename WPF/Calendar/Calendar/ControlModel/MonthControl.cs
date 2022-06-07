using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calendar.ControlModel
{
    public class MonthControl
    {
        #region Properties

        public List<DayControl> DayControls { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Loads all DayControls, which are included in the MonthGrid.
        /// </summary>
        /// <param name="monthGrid">Grid of Month</param>
        public void LoadControls(Grid monthGrid)
        {
            try
            {
                if (null == DayControls)
                {
                    DayControls = new List<DayControl>();
                }
                foreach (var item in monthGrid.Children)
                {
                    DayControl dayControl = item as DayControl;
                    if (dayControl != null)
                    {
                        dayControl.Date = new DateTime(1, 1, 1);
                        DayControls.Add(dayControl);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MonthControl.LoadControls()".ToUpper());
                Debug.Indent();
                Debug.WriteLine("Error during loading of \"DayControls\":");
                Debug.WriteLine(ex);
                Debug.Unindent();
                Debugger.Break();
            }
        

        }

        /// <summary>
        /// Configures the complete MonthGrid -> for one month in 3 important steps:
        /// 1) Configure controls, which should not be displayed (before the first day of the month)
        /// 2) Configure date controls which should be visible, with context of a day
        /// 3) Configure controls, which should not be displayed (after the last day of the month), 
        ///    in case of none of the last 7 day-controls will without a value, set the RowDefinition of the last row to 0, to hide this row
        /// </summary>
        /// <param name="startIndex">Control which should display the first day of the month</param>
        /// <param name="dates">all dates of a month</param>
        /// <param name="rowDef">RowDefinition which should be set to 0 (not displayed) in case of none of the last 7 day-controls will not be filled.</param>
        /// <returns>true/false</returns>
        public bool ConfigureDayControls(int startIndex, List<DateTime> dates, RowDefinition rowDef)
        {
            int dateCounter = 1;

            try
            {
                /// Configure controls, which should not be displayed (before the first day of the month
                for (int i = 0; i < startIndex; i++)
                {
                    DayControls[i].Visibility = Visibility.Hidden;
                }

                /// Configure date controls (Also set visibility to visible) -> stop by last day of month
                for (int i = startIndex; i < (dates.Count + startIndex); i++)
                {
                    DayControls[i].Visibility = Visibility.Visible;
                    DayControls[i].Date = dates.Where(x => x.Date.Day == dateCounter).FirstOrDefault();
                    DayControls[i].SetDateOnLabel(DayControls[i].Date);

                    if (DayControls[i].Name.StartsWith("mSon"))
                        DayControls[i].StyleLabelSunday();
                    else
                        DayControls[i].StyleLabelDay();

                    dateCounter++;
                }

                int restDayControls = DayControls.Count() - (dates.Count() + startIndex);
                if (restDayControls >= 7)
                {
                    GridLengthConverter glc = new GridLengthConverter();
                    rowDef.Height = (GridLength)glc.ConvertFrom("0*");
                }
                else
                {
                    GridLengthConverter glc = new GridLengthConverter();
                    rowDef.Height = (GridLength)glc.ConvertFrom("1*");
                }

                /// Configure controls, which should not be displayed (after the last day of the month)

                for (int i = (startIndex + dates.Count()); i < DayControls.Count; i++)
                {
                    DayControls[i].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MonthControl.ConfigureDayControls()".ToUpper());
                Debug.Indent();
                Debug.WriteLine("Error during configuring days:");
                Debug.WriteLine(ex);
                Debug.Unindent();
                Debugger.Break();
                return false;
            }

            return false;
        }

        /// <summary>
        /// Deselcts a day-control
        /// </summary>
        /// <param name="oldDate">current selected control, which should be deselected</param>
        /// <returns>Success: 0 else the old selected day</returns>
        public DateTime DeselectDayControl(DateTime oldDate)
        {
            try
            {
                DayControl odc = DayControls.Where(x => x.Date.Day == oldDate.Day).FirstOrDefault();

                if (odc != null)
                {
                    if (odc.Name.StartsWith("mSon"))
                        odc.StyleDayControlSunday();
                    else
                        odc.StyleDayControl();

                    return new DateTime();
                }
                return oldDate;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MonthControl.DeSelectDayControl()".ToUpper());
                Debug.Indent();
                Debug.WriteLine("Error during configuring days:");
                Debug.WriteLine(ex);
                Debug.Unindent();
                Debugger.Break();
                return oldDate;
            }
        }

        /// <summary>
        /// Changes the selection of the day control.
        /// 1) Deselect the the current control
        /// 2) Select the new control
        /// </summary>
        /// <param name="oldDate">currently selected control</param>
        /// <param name="currentDate">new selected control</param>
        /// <returns>Success: current selected day/ Failed: 0</returns>
        public DateTime SelectDayControl(DateTime oldDate, DateTime currentDate)
        {
            try
            {
                DeselectDayControl(oldDate);

                DayControl ndc = DayControls.Where(x => x.Date.Date == currentDate.Date).FirstOrDefault();

                if (ndc != null)
                {
                    ndc.StyleSelectDayControl();
                }

                // return the currently selected day!
                return ndc.Date;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MonthControl.SelectDayControl()".ToUpper());
                Debug.Indent();
                Debug.WriteLine("Error during configuring days:");
                Debug.WriteLine(ex);
                Debug.Unindent();
                Debugger.Break();
                return new DateTime();
            }
        }

        #endregion


    }
}
