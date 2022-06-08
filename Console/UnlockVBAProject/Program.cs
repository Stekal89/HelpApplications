using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// References:
// Microsoft.Vbe.Interop
// Microsoft.Office.Interop.Excel
using Excel = Microsoft.Office.Interop.Excel;
using VBA = Microsoft.Vbe.Interop;

namespace UnlockVBAProject
{
    internal class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        static void Main(string[] args)
        {
            // File which should be loaded
            string XlsFilename = @"C:\temp\ProtectedTestTemplate.xltm";
            // Pasword which should be used to open the VBA-Project (Modules are included there)
            string VBACodePassword = "MyTestPassword";

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

                objWorkbook = objExcel.Workbooks.Open(XlsFilename, false, false, Type.Missing, Type.Missing, Type.Missing,
                                true, Type.Missing, Type.Missing, false, false, Type.Missing, false, true, Type.Missing);

                var objProject = objWorkbook.VBProject;
                var projectName = objProject.Name;
                if (objProject.Protection == Microsoft.Vbe.Interop.vbext_ProjectProtection.vbext_pp_locked)
                {
                    //
                    //  Need to somehow pass the .xlsm password to open the VBA code.
                    // 
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

                    SendKeys.SendWait(VBACodePassword);
                    System.Threading.Thread.Sleep(pauseTime);

                    SendKeys.SendWait("{ENTER}");

                    System.Threading.Thread.Sleep(pauseTime);
                    SendKeys.SendWait("{ESCAPE}");

                    objExcel.VBE.MainWindow.Visible = false;
                    objProject.VBE.MainWindow.Visible = false;
                }

                foreach (var component in objProject.VBComponents)
                {
                    objModules = objExcel.ActiveWorkbook.VBProject.VBComponents;
                    if (null != objModules && objModules.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"\n#############################");
                        Console.WriteLine($"\n#############################");

                        for (int i = 1; i <= objModules.Count; i++)
                        {
                            System.Diagnostics.Debug.WriteLine($"\n-----------------------------");
                            System.Diagnostics.Debug.WriteLine($"Module: \"{objModules.Item(i).Name}\"");
                            System.Diagnostics.Debug.WriteLine($"Type: \"{objModules.Item(i).Type}\"");
                            System.Diagnostics.Debug.WriteLine($"CodeModule.CountOfLines: \"{objModules.Item(i).CodeModule.CountOfLines}\"");

                            Console.WriteLine($"\n-----------------------------");
                            Console.WriteLine($"Module: \"{objModules.Item(i).Name}\"");
                            Console.WriteLine($"Type: \"{objModules.Item(i).Type}\"");
                            Console.WriteLine($"CodeModule.CountOfLines: \"{objModules.Item(i).CodeModule.CountOfLines}\"");

                            if (objModules.Item(i).CodeModule.CountOfLines > 0)
                            {
                                System.Diagnostics.Debug.WriteLine($"CodeModule.CodePane: \"{objModules.Item(i).CodeModule.Lines[1, (objModules.Item(i).CodeModule.CountOfLines)]}\"");
                                Console.WriteLine($"CodeModule.CodePane: \"{objModules.Item(i).CodeModule.Lines[1, (objModules.Item(i).CodeModule.CountOfLines)]}\"");
                            }

                            System.Diagnostics.Debug.WriteLine($"-----------------------------\n");
                            Console.WriteLine($"-----------------------------\n");
                        }

                        System.Diagnostics.Debug.WriteLine($"#############################\n");
                        Console.WriteLine($"#############################\n");
                        //System.Diagnostics.Debugger.Break();
                    }

                }
            }
            catch (Exception ex)
            {
                ConsoleColor curentColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nException during loading of excel file:\n{ex.Message}\n");
                Console.ForegroundColor = curentColor;
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
                    //workbook.Save();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objWorkbook);
                    objWorkbook = null;
                }

                if (null != objExcel)
                {
                    Excel.Workbooks objWorkBooks = objExcel.Workbooks;
                    objWorkBooks.Close();
                    var test = objExcel.ActiveWorkbook.VBProject;
                    
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objWorkBooks);
                    objWorkBooks = null;

                    objExcel.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
                    objExcel = null;
                }
                //  Apparently, these two lines "should" help to make sure the Excel.exe process is killed off.
                //  http://support.microsoft.com/default.aspx?scid=KB;EN-US;317109
                GC.Collect();

            }

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }
    }
}
