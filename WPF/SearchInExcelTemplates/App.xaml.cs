using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

// References:
// Microsoft.Vbe.Interop
// Microsoft.Office.Interop.Excel
using Excel = Microsoft.Office.Interop.Excel;

namespace SearchInExcelTemplates
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // https://stackoverflow.com/questions/12049894/import-code-modules-programmatically-to-excel-workbook

            // References:
            // Microsoft.Vbe.Interop
            // Microsoft.Office.Interop.Excel

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            string filePath = @"C:\tmp\ExcelTemplates\OpenExcelTemplate.xltm";
            Excel.Application objExcel = null;

            try
            {
                objExcel = new Excel.Application();
                objExcel.Visible = true;
                objExcel.Workbooks.Open(filePath);

                if (null != objExcel.Workbooks && objExcel.Workbooks.Count > 0)
                {
                    //var objModules = objExcel.ActiveWorkbook.VBProject.VBComponents;
                    //if (null != objModules && objModules.Count > 0)
                    //{
                    //    foreach (var item in objModules)
                    //    {
                    //        item.impo
                    //        System.Diagnostics.Debug.WriteLine(item);
                    //    }
                    //    System.Diagnostics.Debugger.Break();
                    //}

                    //if (null != objExcel.ActiveWorkbook.VBProject.VBComponents && objExcel.ActiveWorkbook.VBProject.VBComponents.Count > 0)
                    //{
                    //    for (int i = 1; i <= objExcel.ActiveWorkbook.VBProject.VBComponents.Count; i++)
                    //    {
                    //        System.Diagnostics.Debug.WriteLine(objExcel.ActiveWorkbook.VBProject.VBComponents.Item(i).Name);
                    //    }
                    //    System.Diagnostics.Debugger.Break();
                    //}

                    if (!objExcel.ActiveWorkbook.HasVBProject)
                    {
                        return;
                    }

                    if (objExcel.ActiveWorkbook.VBProject.Protection == Microsoft.Vbe.Interop.vbext_ProjectProtection.vbext_pp_locked)
                    {
                        // Unlock, if possible
                        objExcel.ActiveWorkbook.Unprotect("PassWord");
                    }

                    var objModules = objExcel.ActiveWorkbook.VBProject.VBComponents;
                    if (null != objModules && objModules.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"\n#############################");

                        for (int i = 1; i <= objModules.Count; i++)
                        {
                            System.Diagnostics.Debug.WriteLine($"\n-----------------------------");
                            System.Diagnostics.Debug.WriteLine($"Module: \"{objModules.Item(i).Name}\"");
                            System.Diagnostics.Debug.WriteLine($"Type: \"{objModules.Item(i).Type}\"");
                            System.Diagnostics.Debug.WriteLine($"CodeModule.CountOfLines: \"{objModules.Item(i).CodeModule.CountOfLines}\"");
                            if (objModules.Item(i).CodeModule.CountOfLines > 0)
                            {
                                System.Diagnostics.Debug.WriteLine($"CodeModule.CodePane: \"{objModules.Item(i).CodeModule.Lines[1, (objModules.Item(i).CodeModule.CountOfLines)]}\"");
                            }
                           
                            System.Diagnostics.Debug.WriteLine($"-----------------------------\n");
                        }

                        System.Diagnostics.Debug.WriteLine($"#############################\n");
                        System.Diagnostics.Debugger.Break();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(mainWindow, $"Fehler beim lesen der Datei \"{filePath}\"\r\nERROR -> {ex.Message}");
            }
            finally
            {
                if (objExcel != null)
                {
                    objExcel.Workbooks.Close();
                    objExcel.Quit();

                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWb);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
                    //excelWb = null;
                    //excelWorksheet = null;
                    objExcel = null;
                    GC.Collect();
                }
            }

            mainWindow.Close();
           
        }
    }
}
