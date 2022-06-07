using System;
using System.IO;

namespace UserTempDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            // %Temp%
            string strTemp = $@"{Path.GetTempPath()}\MyCompany\Test.txt";
            CheckOrCreateFile(strTemp);

           // %APPDATA%
            string strAppData = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\MyCompany\Test.txt";
            CheckOrCreateFile(strAppData);
        }

        /// <summary>
        /// Verify if file exist, if file does not exist create the file inclussive directory.
        /// </summary>
        /// <param name="strFilePath">Full-File-Path</param>
        public static void CheckOrCreateFile(string strFilePath)
        {
            try
            {
                string strDir = Path.GetDirectoryName(strFilePath);

                /// ##################################################################################
                /// Verify if directory exist, if it does not exist create the dire
                /// ##################################################################################
               
                Console.WriteLine($"Verify if directory \"{strDir}\" exist...");
                if (!Directory.Exists(strDir))
                {
                    Console.WriteLine($"Directory \"{strDir}\" does not exist, create directory...");
                    Directory.CreateDirectory(strDir);
                }

                /// ##################################################################################
                /// Verify if file exist, if it does not exist create the file:
                /// ##################################################################################

                Console.WriteLine($"\n\nVerify if file \"{strFilePath}\" exist...");
                if (!File.Exists(strFilePath))
                {
                    Console.WriteLine($"File \"{strFilePath}\" does not exist, create file...");
                    File.Create(strFilePath);

                    if (File.Exists(strFilePath))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"File \"{strFilePath}\" was successfully created");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"File \"{strFilePath}\" allready exist");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Somehting happened:\n{ex.Message}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
