using System;
using System.Collections.Generic;
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
    /// Interaction logic for DayControl.xaml
    /// </summary>
    public partial class DayControl : UserControl
    {
        ResourceDictionary styleRessource = (ResourceDictionary)Application.LoadComponent(new Uri("/Calendar;component/css/StyleSheet.xaml", UriKind.Relative));

        public DateTime Date { get; set; }

        public bool IsSelected { get; set; }

        public DayControl()
        {
            InitializeComponent();
            //Date = DateTime.Now;
            //SetDateOnLabel(Date);
        }

        #region Functions

        #region StyleFunctions

        public void StyleLabelSunday()
        {
            lblDay.Style = styleRessource["labelSunday"] as Style;
        }

        public void StyleLabelDay()
        {
            lblDay.Style = styleRessource["labelDay"] as Style;
        }

        public void StyleDayControl()
        {
            Style defStyle = styleRessource["dayGridSunday"] as Style;
            grdDay.Style = defStyle;
            IsSelected = false;
        }

        public void StyleDayControlSunday()
        {
            Style defStyle = styleRessource["dayGrid"] as Style;
            grdDay.Style = defStyle;
            IsSelected = false;
        }

        public void StyleSelectDayControl()
        {
            Style clickedStyle = styleRessource["selectedDayGrid"] as Style;
            grdDay.Style = clickedStyle;
            IsSelected = true;
        }

        public void StyleWeekDayHeader()
        {
           lblDay.Style = styleRessource["weekDayHeader"] as Style;
            // Content Placeholder
            lblDay.Content = "dd.mm";
        }

        public void StyleWeekDayHeaderSunday()
        {
            lblDay.Style = styleRessource["weekDaySundayHeader"] as Style;
            // Content Placeholder
            lblDay.Content = "dd.mm";
        }

        #endregion

        #region Other Functions

        public void SetDateOnLabel(DateTime date)
        {
            lblDay.Content = date.Day.ToString();
        }


        private void ShowDay(object sender, FilterEventArgs e)
        {

        }

        private void ClickOnDayControl(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion

        #endregion




    }
}
