using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_RunSilently.Model
{
    public static class Logging
    {
        private static readonly string m_strLogFilePath = $@"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\log\WPF_RunSilently.log";

        /// <summary>
        /// Logging in case of Silent-Mode
        /// </summary>
        /// <param name="strParameters"></param>
        public static void LogParameters(string[] strParameters)
        {
            if (VerifyOrCreateLogFile())
            {
                File.AppendAllText(m_strLogFilePath, $"LogParameters() => {String.Join(", ", strParameters)}\r\n");
            }
        }

        /// <summary>
        /// Logging in case of MainWindow-Mode
        /// </summary>
        public static void LogFromMainWindow()
        {
            if (VerifyOrCreateLogFile())
            {
                File.AppendAllText(m_strLogFilePath, "LogFromMainWindow() => Call from MainWindow!\r\n");
            }
        }

        /// <summary>
        /// Verify if Log-File exist, if Log-File does not exist create it!
        /// </summary>
        /// <returns>Success/Fail</returns>
        private static bool VerifyOrCreateLogFile()
        {
            try
            {
                /// ##################################################################################
                /// Check if file exist
                /// ##################################################################################

                if (!System.IO.File.Exists(m_strLogFilePath))
                {
                    /// ##################################################################################
                    /// Check if directory of file exist, if not create the directory:
                    /// ##################################################################################

                    if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(m_strLogFilePath)))
                    {
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(m_strLogFilePath));
                    }

                    /// ##################################################################################
                    /// Create file:
                    /// ##################################################################################

                    System.IO.File.Create(m_strLogFilePath).Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"VerifyOrCreateLogFile() => Something happens during verification/creation of file, or directory:\n\"{ex.Message}\"");
                return false;
            }
        }
    }
}
