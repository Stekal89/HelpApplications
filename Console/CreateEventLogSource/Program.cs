using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateEventLogSource
{
    class Program
    {
        static void Main(string[] args)
        {
            bool bExit = false;
            do
            {
                Console.Write("Please enter the applicationname: ");
                string strAppName = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(strAppName.Trim()))
                {
                    using (System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog())
                    {
                        // Check if Event-Source already exist, if not create them
                        if (!System.Diagnostics.EventLog.SourceExists(strAppName))
                        {
                            Console.WriteLine($"Eventsource \"{strAppName}\" does not exist.");
                            Console.WriteLine($"Create Eventsource \"{strAppName}\"...");
                            System.Diagnostics.EventLog.CreateEventSource(strAppName, "Application");
                        }
                        else
                        {
                            Console.WriteLine($"Eventsource \"{strAppName}\" already exist.");
                        }

                        // Set the Source-Name
                        appLog.Source = strAppName;

                        // Close the Event-Log
                        appLog.Close();
                    }

                    bExit = true;
                }
                else
                {
                    Console.WriteLine("Input is Null or empty!");
                    Console.WriteLine("\n\nContinue with any key...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (!bExit);

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }
    }
}
