using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchInExcelTemplates.Models
{
    public enum eFolderOrFilePath
    {
        Nothing,
        Folder,
        File
    }

    public enum eOfficeLanguage
    {
        German,
        English
    }
    public class SearchConfiguration
    {
        public string SearchParameter { get; set; }

        public string Path { get; set; }

        public string Filter { get; set; }

        public string PasswordAsPlainText { get; set; }
        public bool MatchCase { get; set; }

        public eFolderOrFilePath FolderOrFilePath { get; set; } = eFolderOrFilePath.Nothing;

        /// <summary>
        /// Will be needed for the Short-Key to open the Password-Dialog in VBA Editor
        /// </summary>
        public eOfficeLanguage OfficeLanguage { get; set; }

        #region Functions

        /// <summary>
        /// Validate Input-FilePath and check if it is a folder, or a file.
        /// Verify if folderpath/filepath exist.
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <returns>Input-Path is valid => True/Input-Path is NOT valid, or error => False</returns>
        public bool ValidatePath(out string error)
        {
            error = null;

            try
            {
                // get the file attributes for file or directory
                System.IO.FileAttributes attr = System.IO.File.GetAttributes(Path);

                //detect whether its a directory or file
                if ((attr & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
                {
                    if (System.IO.Directory.Exists(Path))
                    {
                        FolderOrFilePath = eFolderOrFilePath.Folder;
                        return true;
                    }
                    else
                    {
                        error += $"Verzeichnis \"{Path}\" existiert nicht!\r\n\r\n";
                    }
                }
                else
                {
                    if (System.IO.File.Exists(Path))
                    {
                        FolderOrFilePath = eFolderOrFilePath.File;
                        return true;
                    }
                    else
                    {
                        error += $"Dateipfad \"{Path}\" existiert nicht!\r\n\r\n";
                    }
                }
            }
            catch (Exception ex)
            {
                error += $"IsFileOrFolderPath-ERROR: {ex.Message}\r\n\r\n";
            }

            return false;
        }

        public bool SearchInFiles(out string error)
        {
            // File, or folder?
            error = null;
            string er = null;

            MainWindow.FileResults = new List<FileResult>();
            FileResult objFileResult = null;

            switch (FolderOrFilePath)
            {
                case eFolderOrFilePath.Nothing:
                    return false;
                case eFolderOrFilePath.Folder:

                    System.Diagnostics.Debug.WriteLine($"\n#################################");

                    string[] extensions = null;
                    if (!string.IsNullOrEmpty(Filter))
                    {
                        Filter = Filter.Replace("*", null).Replace(" ", null).Trim();

                        if (Filter.Contains(","))
                        {
                            extensions = Filter.Split(',');
                        }
                        else if (Filter.Contains(";"))
                        {
                            extensions = Filter.Split(';');
                        }
                        else if (Filter.Contains("|"))
                        {
                            extensions = Filter.Split('|');
                        }
                        else
                        {
                            extensions = new string[] { Filter };
                        }
                    }
                    else
                    {
                        extensions = new string[] { ".xlsm", ".xlsb", ".xlt", ".xltm" };
                    }

                    List<string> liOfFiles = Directory.EnumerateFiles(Path, "*.*", SearchOption.AllDirectories).Where(s => extensions.Any(ext => ext == System.IO.Path.GetExtension(s))).ToList();

                    if (null == liOfFiles || liOfFiles.Count <= 0)
                    {
                        return false;
                    }

                    bool bSuccess = true;

                    MainWindow.ProcessStatus = eProcessStatus.SearchForContent;
                    MainWindow.m_ActFile = 1;
                    MainWindow.m_MaxFile = liOfFiles.Count;

                    foreach (string file in liOfFiles)
                    {
                        //System.Diagnostics.Debug.WriteLine($"\n----------------------------------");
                        //System.Diagnostics.Debug.WriteLine(file);
                        //System.Diagnostics.Debug.WriteLine($"----------------------------------\n");

                        objFileResult = new FileResult(file);
                        if(!objFileResult.SearchInFile(out er, this))
                        {
                            bSuccess = false;
                        }
                        else
                        {
                            MainWindow.m_ReadSuccessfully++;
                        }
                        error += er;

                        //MainWindow.FileResults.Add(objFileResult);
                        MainWindow.m_ActFile++;
                    }

                    return bSuccess;
                    //System.Diagnostics.Debug.WriteLine($"#################################\n");
                    //System.Diagnostics.Debugger.Break();

                case eFolderOrFilePath.File:

                    MainWindow.ProcessStatus = eProcessStatus.SearchForContent;

                    MainWindow.m_ActFile = 1;
                    MainWindow.m_MaxFile = 1;

                    objFileResult = new FileResult(Path);
                    if (!objFileResult.SearchInFile(out er, this))
                    {
                        error += er;
                        return false;
                    }

                    //MainWindow.FileResults.Add(objFileResult);
                    MainWindow.m_ReadSuccessfully++;
                    return true;
            }

            // Load all existing templates in the path

            // For each template:

            // Load Template

            // Verify if the word, where we looking for are exist in one of the lodaed Templates

            return false;
        }

        #endregion
    }
}
