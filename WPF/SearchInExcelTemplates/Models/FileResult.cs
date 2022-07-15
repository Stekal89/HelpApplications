using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// References:
// Microsoft.Vbe.Interop -> Microsoft Excel Object Library
// Microsoft.Office.Interop.Excel
using Excel = Microsoft.Office.Interop.Excel;
using VBA = Microsoft.Vbe.Interop;

namespace SearchInExcelTemplates.Models
{
    public class FileResult
    {
        //[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        //static extern bool BringWindowToTop(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        
        public string FilePath { get; set; }

        public bool ModuleExist { get; set; } = false;

        public bool Protected { get; set; } = false;

        public bool SearchMatch { get; set; } = false;

        public string ModuleName { get; set; }

        public int RowNumber { get; set; }

        public string RowContent { get; set; }

        /// <summary>
        /// ModuleName, RowNumber, RowContent 
        /// </summary>
        //public List<(string, int, string )> RowInfos { get; set; } = null;

        public string InfoMsg { get; set; } = null;

        #region Constructors

        public FileResult()
        {

        }

        public FileResult(string strFilePath)
        {
            FilePath = strFilePath;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Load all Modules from the current Excel-File and look for matches.
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="objSearchConfiguration">User-Configuration</param>
        /// <returns></returns>
        public bool SearchInFile(out string error, SearchConfiguration objSearchConfiguration)
        {
            // https://stackoverflow.com/questions/12049894/import-code-modules-programmatically-to-excel-workbook

            // References:
            // Microsoft.Vbe.Interop
            // Microsoft.Office.Interop.Excel

            error = null;

            if (string.IsNullOrEmpty(FilePath))
            {
                error += $"Kein Dateipfad angegeben!";
                return false;
            }

            Excel.Application objExcel = null;
            Excel.Workbook objWorkbook = null;
            VBA.VBComponents objModules = null;

            //System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
            //eventLog.Source = "SearchInExcelTemplates";

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

                objWorkbook = objExcel.Workbooks.Open(FilePath, false, false, Type.Missing, Type.Missing, Type.Missing,
                                true, Type.Missing, Type.Missing, false, false, Type.Missing, false, true, Type.Missing);

                var objProject = objWorkbook.VBProject;

                if (null != objExcel.Workbooks && objExcel.Workbooks.Count > 0)
                {
                    if (!objWorkbook.HasVBProject)
                    {
                        InfoMsg += $"Datei \"{FilePath}\" hat kein VB-Projekt.";
                        return true;
                    }

                    // Verify if VB-Procject is protected
                    if (objWorkbook.VBProject.Protection == Microsoft.Vbe.Interop.vbext_ProjectProtection.vbext_pp_locked)
                    {
                        Protected = true;

                        if (string.IsNullOrEmpty(objSearchConfiguration.PasswordAsPlainText))
                        {
                            error += $"Datei \"{FilePath}\" ist Passwort geschützt, es ";
                            return false;
                        }

                        // Unlock, if possible
                        objWorkbook.Activate(); //Activate the correct WorkBook.

                        objExcel.VBE.MainWindow.Visible = true;
                        objProject.VBE.MainWindow.Visible = true;
                        objProject.VBE.MainWindow.SetFocus();

                        SetForegroundWindow((IntPtr)objProject.VBE.MainWindow.HWnd);

                        objExcel.VBE.ActiveVBProject = objProject;
                        int pauseTime = 1000;

                        SendKeys.SendWait("%{F10}");
                        //objProject.VBE.MainWindow.SetFocus();
                        //SetForegroundWindow((IntPtr)objProject.VBE.MainWindow.HWnd);
                        System.Threading.Thread.Sleep(pauseTime);

                        switch (objSearchConfiguration.OfficeLanguage)
                        {
                            case eOfficeLanguage.German:
                                SendKeys.SendWait("%XI");
                                break;
                            case eOfficeLanguage.English:
                                SendKeys.SendWait("%TE");
                                break;
                        }
                        
                        System.Threading.Thread.Sleep(pauseTime);

                        SendKeys.SendWait(objSearchConfiguration.PasswordAsPlainText);
                        System.Threading.Thread.Sleep(pauseTime);

                        SendKeys.SendWait("{ENTER}");
                        System.Threading.Thread.Sleep(pauseTime);

                        SendKeys.SendWait("{ESCAPE}");

                        objExcel.VBE.MainWindow.Visible = false;
                        objProject.VBE.MainWindow.Visible = false;
                    }

                    // Load all modules of VB-Procject
                    objModules = objExcel.ActiveWorkbook.VBProject.VBComponents;
                    if (null != objModules && objModules.Count > 0)
                    {
                        ModuleExist = true;

                        for (int i = 1; i <= objModules.Count; i++)
                        {
                            if (objModules.Item(i).CodeModule.CountOfLines > 0)
                            {
                                // Load all rows of module
                                if (!objSearchConfiguration.MatchCase)
                                {
                                    if (objModules.Item(i).CodeModule.Lines[1, (objModules.Item(i).CodeModule.CountOfLines)].ToUpper().Contains(objSearchConfiguration.SearchParameter.ToUpper()))
                                    {
                                        SearchMatch = true;

                                        for (int r = 1; r < objModules.Item(i).CodeModule.CountOfLines; r++)
                                        {
                                            if (objModules.Item(i).CodeModule.Lines[r, 1].ToUpper().Contains(objSearchConfiguration.SearchParameter.ToUpper()))
                                            {
                                                //if (null == RowInfos)
                                                //{
                                                //    RowInfos = new List<(string, int, string)>();
                                                //}

                                                //string strTmpValue = objModules.Item(i).CodeModule.Lines[r, 1];
                                                //RowInfos.Add((objModules.Item(i).Name, r, strTmpValue));

                                                ModuleName = objModules.Item(i).Name;
                                                RowNumber = r;
                                                RowContent = objModules.Item(i).CodeModule.Lines[r, 1];
                                                MainWindow.FileResults.Add(new FileResult()
                                                {
                                                    FilePath = this.FilePath,
                                                    ModuleExist = this.ModuleExist,
                                                    Protected = this.Protected,
                                                    SearchMatch = this.SearchMatch,
                                                    ModuleName = this.ModuleName,
                                                    RowNumber = this.RowNumber,
                                                    RowContent = this.RowContent
                                                }); 
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // exact Search
                                    if (objModules.Item(i).CodeModule.Lines[1, (objModules.Item(i).CodeModule.CountOfLines)].Contains(objSearchConfiguration.SearchParameter))
                                    {
                                        SearchMatch = true;

                                        for (int r = 1; r < objModules.Item(i).CodeModule.CountOfLines; r++)
                                        {
                                            if (objModules.Item(i).CodeModule.Lines[r, 1].Contains(objSearchConfiguration.SearchParameter))
                                            {
                                                //if (null == RowInfos)
                                                //{
                                                //    RowInfos = new List<(string, int, string)>();
                                                //}

                                                //string strTmpValue = objModules.Item(i).CodeModule.Lines[r, 1];
                                                //RowInfos.Add((objModules.Item(i).Name, r, strTmpValue));


                                                ModuleName = objModules.Item(i).Name;
                                                RowNumber = r;
                                                RowContent = objModules.Item(i).CodeModule.Lines[r, 1];
                                                MainWindow.FileResults.Add(new FileResult()
                                                {
                                                    FilePath = this.FilePath,
                                                    ModuleExist = this.ModuleExist,
                                                    Protected = this.Protected,
                                                    SearchMatch = this.SearchMatch,
                                                    ModuleName = this.ModuleName,
                                                    RowNumber = this.RowNumber,
                                                    RowContent = this.RowContent
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"Fehler beim lesen der Datei \"{FilePath}\"\r\nERROR -> {ex.Message}";
                return false;
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

            return false;
        }

            #endregion

        }
}
