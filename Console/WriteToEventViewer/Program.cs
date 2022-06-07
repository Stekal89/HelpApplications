using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteToEventViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Default Message (Unknown Error):
            WriteToEventLog("TestTitle", null, EventLogEntryType.Information);

            // Message without title:
            WriteToEventLog(null, "Error without a title!", EventLogEntryType.Warning);

            // Message with title:
            WriteToEventLog("Heavy Error", "An heavy Error appears during testing!\r\n" +
                                                            "Please check your input again.", EventLogEntryType.Error);

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }

        /// <summary>
        /// Get Information if the current Process is running as Administrator.
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Creates the Message, which will be written into the Event-Log.
        /// </summary>
        /// <param name="strNewLines">New-Lines</param>
        /// <param name="strProcessName">Process-Name</param>
        /// <param name="strTitle">Title</param>
        /// <param name="strText">Message</param>
        /// <returns>Complete Message</returns>
        private static string CreateMessageString(string strNewLines, string strProcessName, string strTitle, string strText)
        {
            if (!string.IsNullOrEmpty(strText))
            {
                if (!string.IsNullOrEmpty(strTitle))
                {
                    return $"{strNewLines}{strProcessName}:\r\n\r\n{strTitle}\r\n\r\n{strText}";
                }
                else
                {
                    return $"{strNewLines}{strProcessName}:\r\n\r\n{strText}";
                }
            }
            else
            {
                return $"{strNewLines}{strProcessName}:\r\n\r\nUnknown Error!";
            }
        }

        /// <summary>
        /// Write to EventLog
        /// </summary>
        /// <param name="strTitle">Event-Title</param>
        /// <param name="strText">Message</param>
        /// <param name="eventLogEntryType">Type of EventLog -> Error, Information, Warning,...</param>
        private static void WriteToEventLog(string strTitle, string strText, System.Diagnostics.EventLogEntryType eventLogEntryType)
        {
            // Create an instance of EventLog
            using (System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog())
            {
                // Get name of the current Process
                string strProcessName = Process.GetCurrentProcess().ProcessName;
                // Create an event ID to add to the event log (I will use the ProcessId)
                int eventID = Process.GetCurrentProcess().Id;
                string strNewLines = null;

                if (IsAdministrator())
                {
                    // Check if the event source exists. If not create it.
                    if (!System.Diagnostics.EventLog.SourceExists(strProcessName))
                    {
                        System.Diagnostics.EventLog.CreateEventSource(strProcessName, "Application");
                    }

                    // Set the source name for writing log entries.
                    appLog.Source = strProcessName;
                }
                else
                {
                    appLog.Source = "Application";
                    strNewLines = "\r\n\r\n";
                }
                appLog.WriteEntry(CreateMessageString(strNewLines, strProcessName,strTitle, strText), eventLogEntryType, eventID);
                    
                // Close the Event Log
                appLog.Close();
            }
        }
    }
}
