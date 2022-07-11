using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

// References:
// Microsoft.Vbe.Interop -> Microsoft Excel Object Library
// Microsoft.Office.Interop.Excel
using Excel = Microsoft.Office.Interop.Excel;
using VBA = Microsoft.Vbe.Interop;

namespace SearchInExcelTemplates
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string AppName = "SearchInExcelTemplates";

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // https://stackoverflow.com/questions/12049894/import-code-modules-programmatically-to-excel-workbook

            // References:
            // Microsoft.Vbe.Interop
            // Microsoft.Office.Interop.Excel

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            //System.Diagnostics.Debugger.Break();

            //mainWindow.Close();

        }
    }
}
