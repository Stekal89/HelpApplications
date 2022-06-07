using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadAndSortFiles
{
    class Program
    {
        #region Help-Functions

        /// <summary>
        /// Writes Console-Output in DarkGreen
        /// </summary>
        /// <param name="strMsg">Message</param>
        private static void WriteSuccess(string strMsg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n{strMsg}");
            Console.ForegroundColor = currentColor;
        }

        /// <summary>
        /// Writes Console-Output in DarkYellow
        /// </summary>
        /// <param name="strMsg">Message</param>
        private static void WriteWarning(string strMsg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n{strMsg}");
            Console.ForegroundColor = currentColor;
        }

        /// <summary>
        /// Writes Console-Output in Red
        /// </summary>
        /// <param name="strMsg">Message</param>
        private static void WriteError(string strMsg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{strMsg}");
            Console.ForegroundColor = currentColor;
        }

        #endregion

        static void Main(string[] args)
        {
            // Load all files of directory ordered by CreationDate with the name like "Test"
            List<string> liOfFilePaths = LoadAllFilesFromDirectory(@"C:\temp\", "%Test%");

            foreach (var item in liOfFilePaths)
            {
                Console.WriteLine($"File: \"{item}\"");
            }

            // Load all files of directory ordered by CreationDate with the name like "Test"
            liOfFilePaths = LoadAllFilesFromDirectory(@"C:\temp\", "*Test*");
            foreach (var item in liOfFilePaths)
            {
                Console.WriteLine($"File: \"{item}\"");
            }

            // Load all files of directory ordered by CreationDate with the name like "Test"
            liOfFilePaths = LoadAllFilesFromDirectory(@"C:\temp\", "Test");

            foreach (var item in liOfFilePaths)
            {
                Console.WriteLine($"File: \"{item}\"");
            }

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }


        /// <summary>
        /// Loads all files of the given directory sorted by CreationDate and returns it
        /// as List of full file names.
        /// </summary>
        /// <param name="strFilePath">Path/Directory, where the files are located</param>
        /// <param name="strFiterFileName">files, which should be loaded by name</param>
        /// <returns>List of full file names/NULL</returns>
        public static List<string> LoadAllFilesFromDirectory(string strFilePath, string strFiterFileName)
        {
            List<string> liOfFilePaths = null;

            if (string.IsNullOrEmpty(strFilePath))
            {
                WriteError("Parameter Path is null, or empty!");
                return null;
            }
            if (string.IsNullOrEmpty(strFiterFileName))
            {
                WriteError("Parameter FileName is null, or empty!");
                return null;
            }

            strFiterFileName = strFiterFileName.ToLower();

            try
            {
                // Check if Path exist
                if (!System.IO.Directory.Exists(strFilePath))
                {
                    WriteError($"Cannot find path: \"{strFilePath}\"");
                    return null;
                }

                // Sort/Order files in an List of FileiInfo Objects
                System.IO.DirectoryInfo objDirectoryInfo = new System.IO.DirectoryInfo(strFilePath);
                List<System.IO.FileInfo> objFileInfos = objDirectoryInfo.GetFiles().OrderBy(p => p.CreationTime).ToList();

                // Load files which are named like the parameter file
                liOfFilePaths = objFileInfos.Select(fn => fn.FullName).Where(x => System.IO.Path.GetFileName(x).ToLower().Contains(strFiterFileName.Replace("%", "").Replace("*", ""))).ToList();

                if (null != liOfFilePaths && liOfFilePaths.Count > 0)
                {
                    WriteSuccess($"Found files like \"{strFiterFileName.Replace("%", "*")}\"");
                }

            }
            catch (Exception ex)
            {
                WriteError($"\nError during loading list of files!\n{ex.Message}");
            }
            
            return liOfFilePaths;
        }
    }
}
