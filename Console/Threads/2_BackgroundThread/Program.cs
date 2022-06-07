using System;
using System.Threading;

namespace _2_BackgroundThread
{
    class Program
    {
        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"ThreadProc: \"{i}\"");
                Thread.Sleep(1000);
            }
        }

        static void Main(string[] args)
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            // If you run this application with the IsBackground property set to true, the 
            // application exits immediately.If you set it to false(creating a foreground 
            // thread), the application prints the ThreadProc message ten times.
            t.IsBackground = true;
            t.Start();

            Console.WriteLine("\n\nContinue with any key...\n");
            Console.ReadKey();
        }
    }
}
