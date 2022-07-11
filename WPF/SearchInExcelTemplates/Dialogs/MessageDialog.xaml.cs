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
using System.Windows.Shapes;

namespace SearchInExcelTemplates.Dialogs
{
    /// <summary>
    /// Interaction logic for MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : Window
    {
        public MessageDialog(string message, string title)
        {
            InitializeComponent();
            tbMessage.Text = message;

            if (!string.IsNullOrEmpty(title))
            {
                window.Title = title;
            }
        }

        /// <summary>
        /// Start this dialog in the center of the window/dialog which where the current Dialog (MessageDialog) was opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Application curApp = System.Windows.Application.Current;
            Window mainWindow = curApp.MainWindow;

            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }

        /// <summary>
        /// Close the Dialog on pressing the Enter-Key while OK-Button was clicked, or focus is on OK-Button, or
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Close the Dialog on pressing the Enter-Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridTouchEnter(object sender, TouchEventArgs e)
        {
            Close();
        }
    }
}
