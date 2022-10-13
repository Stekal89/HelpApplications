using NetworkPathMapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPathMapping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!DriveSettings.IsDriveMapped("L"))
                {
                    Console.WriteLine("");
                    DriveSettings.MapNetworkDrive("L", @"\\%ComputerName%\%ShareName%");
                }
                else
                {
                    DriveSettings.DisconnectNetworkDrive("L", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during network path un/mapping action!\n{ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
