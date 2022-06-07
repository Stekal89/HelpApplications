using System;
using System.Threading;

namespace _01_Threads
{
    class Program
    {
        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"ThreadProc: \"{i}\"");
                // Thread.Sleep is used to signal to Windows that this thread is finished. 
                // Instead of waiting for the whole time-slice of the thread to finish, it 
                // will immediately switch to another thread.
                Thread.Sleep(0);
            }
        }

        static void Main(string[] args)
        {
            // Create Thread using the before created ThreadMethod
            Thread t = new Thread(new ThreadStart(ThreadMethod));

            // Start Thread:
            t.Start();

            // Execute some code in the default Thread (Main-Thread)
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work.");
                // Thread.Sleep is used to signal to Windows that this thread is finished. 
                // Instead of waiting for the whole time-slice of the thread to finish, it 
                // will immediately switch to another thread.
                Thread.Sleep(0);
            }

            t.Join();

            Console.ReadKey();
        }
    }
}
