using System;
using System.IO;

namespace GetCurrentDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            string strExeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string strWorkingDir = Directory.GetCurrentDirectory();

            Console.WriteLine($"Application-Dir: \"{strExeDir}\"");
            Console.WriteLine($"Working-Dir: \"{strWorkingDir}\"");

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }
    }
}
