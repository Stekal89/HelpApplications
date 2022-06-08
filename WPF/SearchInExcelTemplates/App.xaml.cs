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
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // https://stackoverflow.com/questions/12049894/import-code-modules-programmatically-to-excel-workbook

            // References:
            // Microsoft.Vbe.Interop
            // Microsoft.Office.Interop.Excel

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            //string strFilePath = @"C:\tmp\ExcelTemplates\OpenExcelTemplate.xltm";
            string strFilePath = @"C:\tmp\ExcelTemplates\OpenProtectedExcelTemplate.xltm";
            string strPassword = "MyTestPassWord";
            Excel.Application objExcel = null;
            Excel.Workbook objWorkbook = null;
            VBA.VBComponents objModules = null;

            try
            {
                objExcel = new Excel.Application()
                {
                    DisplayAlerts = false,
                    Visible = false,
                    EnableEvents = false
                };

                //  When we open our Excel file, we DON'T want the VBA to start running
                objExcel.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityForceDisable;

                objWorkbook = objExcel.Workbooks.Open(strFilePath, false, false, Type.Missing, Type.Missing, Type.Missing,
                                true, Type.Missing, Type.Missing, false, false, Type.Missing, false, true, Type.Missing);

                var objProject = objWorkbook.VBProject;

                if (null != objExcel.Workbooks && objExcel.Workbooks.Count > 0)
                {
                    if (!objWorkbook.HasVBProject)
                    {
                        return;
                    }

                    if (objWorkbook.VBProject.Protection == Microsoft.Vbe.Interop.vbext_ProjectProtection.vbext_pp_locked)
                    {
                        // Unlock, if possible
                        objWorkbook.Activate(); //Activate the correct WorkBook.

                        objExcel.VBE.MainWindow.Visible = true;
                        objProject.VBE.MainWindow.Visible = true;
                        objProject.VBE.MainWindow.SetFocus();

                        BringWindowToTop((IntPtr)objProject.VBE.MainWindow.HWnd);


                        objExcel.VBE.ActiveVBProject = objProject;
                        int pauseTime = 100;

                        SendKeys.SendWait("%{F10}");
                        System.Threading.Thread.Sleep(pauseTime);

                        SendKeys.SendWait("%TE");
                        System.Threading.Thread.Sleep(pauseTime);

                        SendKeys.SendWait(strPassword);
                        System.Threading.Thread.Sleep(pauseTime);

                        SendKeys.SendWait("{ENTER}");

                        System.Threading.Thread.Sleep(pauseTime);
                        SendKeys.SendWait("{ESCAPE}");

                        objExcel.VBE.MainWindow.Visible = false;
                        objProject.VBE.MainWindow.Visible = false;
                    }

                    objModules = objExcel.ActiveWorkbook.VBProject.VBComponents;
                    if (null != objModules && objModules.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"\n#############################");

                        for (int i = 1; i <= objModules.Count; i++)
                        {
                            System.Diagnostics.Debug.WriteLine($"\n-----------------------------");
                            System.Diagnostics.Debug.WriteLine($"Module: \"{objModules.Item(i).Name}\"");
                            System.Diagnostics.Debug.WriteLine($"Type: \"{objModules.Item(i).Type}\"");
                            System.Diagnostics.Debug.WriteLine($"CodeModule.CountOfLines: \"{objModules.Item(i).CodeModule.CountOfLines}\"\n");
                            if (objModules.Item(i).CodeModule.CountOfLines > 0)
                            {
                                // Load all rows of module
                                System.Diagnostics.Debug.WriteLine($"CodeModule.Lines:\n\n{objModules.Item(i).CodeModule.Lines[1, (objModules.Item(i).CodeModule.CountOfLines)]}");
                            }

                            System.Diagnostics.Debug.WriteLine($"-----------------------------\n");
                        }

                        System.Diagnostics.Debug.WriteLine($"#############################\n");
                    }
                    //System.Diagnostics.Debugger.Break();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(mainWindow, $"Fehler beim lesen der Datei \"{strFilePath}\"\r\nERROR -> {ex.Message}");
            }
            finally
            {
                if (null != objModules)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objModules);
                    objModules = null;
                }
                if (null != objWorkbook)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objWorkbook);
                    objWorkbook = null;
                }

                if (null != objExcel)
                {
                    Excel.Workbooks objWorkBooks = objExcel.Workbooks;
                    objWorkBooks.Close();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objWorkBooks);
                    objWorkBooks = null;

                    objExcel.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
                    objExcel = null;
                }

                GC.Collect();
            }

            System.Diagnostics.Debugger.Break();

            mainWindow.Close();

        }
    }
}
