using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Connection_XAMPP_MySQL
{
    public static class XAMPP
    {
        #region Properties
        
        // Root directory of xampp
        private static string xamppDir = @"C:\xampp";
        // Path of the Apache-Exe to start the Apache Service
        private static string apacheExe = $@"\apache\bin\httpd.exe";
        // Apache Process-Name
        public static string apn = "httpd";

        // Path of the MySql-Exe to start the MySQL Service
        private static string mySqldExe = @"\mysql\bin\mysqld.exe";
        // MySQL Process-Name
        //private static string mspn = "mysqld.exe";
        public static string mspn = "mysqld";

        #endregion

        #region Functions
        
        /// <summary>
        /// Starts the Apache-XAMPP Service.
        /// </summary>
        /// <returns>Apache-Process</returns>
        public static bool StartApache()
        {
            Process[] pList = Process.GetProcessesByName(apn);
            if (pList.Length < 1)
            {
                try
                {
                    ProcessStartInfo pInfo = new ProcessStartInfo();
                    pInfo.FileName = $@"{xamppDir}\{apacheExe}";
                    pInfo.UseShellExecute = false;

                    using (Process apache = new Process())
                    {
                        apache.StartInfo = pInfo;
                        Console.WriteLine("Start \"Apache-Service\"");
                        apache.Start();
                        TimeSpan ts = new TimeSpan(0, 0, 5);
                        Thread.Sleep(ts);

                        return GetProcessInfo(apn); ;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error during startup of \"Apache\"");
                    Console.WriteLine(ex);
                    return false;
                }
            }

            Console.WriteLine("\"Apache-Service\" allready running.");
            GetProcessInfo(apn);
            return true;
        }

        /// <summary>
        /// Starts MySQL-XAMPP Service
        /// </summary>
        /// <returns>Success</returns>
        public static bool StartMySQL()
        {
            Console.WriteLine("\nCheck if Apache-Service is allready running...");
            Process[] apachePList = Process.GetProcessesByName(apn);
            if (!(apachePList.Length >= 1))
                StartApache();
            else
                Console.WriteLine("Apache-Service is allready running.");

            Process[] pList = Process.GetProcessesByName(mspn);
            if (pList.Length < 1)
            {
                try
                {
                    ProcessStartInfo pInfo = new ProcessStartInfo();
                    pInfo.FileName = $@"{xamppDir}\{mySqldExe}";
                    pInfo.UseShellExecute = false;
                    pInfo.WorkingDirectory = $@"{xamppDir}\mysql\bin";
                    pInfo.Arguments = $@"--defaults-file={xamppDir}\mysql\bin\my.ini --standalone --console";
                    //pInfo.UserName = "root";
                    //pInfo.PasswordInClearText = "";

                    using (Process mySql = new Process())
                    {
                        mySql.StartInfo = pInfo;
                        Console.WriteLine("Start \"MySQL-Service\"");
                        mySql.Start();
                        TimeSpan ts = new TimeSpan(0, 0, 5);
                        Thread.Sleep(ts);

                        return GetProcessInfo(mspn);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error during startup of \"MySQL-Service\"");
                    Console.WriteLine(ex);
                    return false;
                }
            }
            Console.WriteLine("\"MySQL-Service\" allready running.");
            GetProcessInfo(mspn);
            return true;
        }

        /// <summary>
        /// Stops XAMPP-Service using his Process-Name
        /// </summary>
        /// <param name="pName">Process-Name</param>
        /// <returns>Success</returns>
        public static bool StopService(string pName)
        {
            try
            {
                Process[] pList = Process.GetProcessesByName(pName);
                if (pList.Length >= 1)
                    foreach (var p in pList)
                    {
                        Console.WriteLine($"Stop Process: \"{p.ProcessName}\" with P-ID: \"{p.Id}\"");
                        p.Kill();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during stopping of \"Apache\"");
                Console.WriteLine(ex);
                return false;
            }

            return false;
        }

        /// <summary>
        /// Shows all the relevant infos of a process in the Console-Window.
        /// </summary>
        /// <param name="pName">Process-Name</param>
        public static bool GetProcessInfo(string pName)
        {
            Process[] pList = Process.GetProcessesByName(pName);
            if (pList.Length >= 1)
            {
                foreach (var p in pList)
                {
                    Console.WriteLine("\n\t##### I N F O : #####");
                    Console.WriteLine($"\tProcess-Name: {p.ProcessName}");
                    Console.WriteLine($"\tProcess-ID: {p.Id}");
                    Console.WriteLine($"\tSession-ID: {p.SessionId}");
                    Console.WriteLine($"\tThreads: {p.Threads.Count}");
                }
                return true;
            }
            return false;
        }

        #endregion
    }
}
