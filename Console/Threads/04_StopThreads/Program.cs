using System;
using System.Threading;

namespace _04_StopThreads
{
    class Program
    {
        public static void ThreadMethod(object o)
        {
            try
            {
                for (int i = 0; i < (int)o; i++)
                {
                    Console.WriteLine($"ThreadProc: \"{i}\"");
                    Thread.Sleep(0);
                }
            }
            catch (ThreadAbortException ex)
            {
                Console.WriteLine($"\n\nThread-Exception: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            // Variante 1: Stop Thread using the Abort function 
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod));
            t.Start(100);


            // Variante 2 (Better way): Stop Thread using a bool
            bool stopped = false;

            // In this case, the thread is initialized with a lambda expression 
            // (which in turn is just a shorthand version of a delegate). The thread 
            // keeps running until stopped becomes true. After that, the t.Join method 
            // causes the console application to wait till the thread finishes execution.
                        Thread t2 = new Thread(new ThreadStart(() =>
            {
                while (!stopped)
                {
                    Console.WriteLine("Running...");
                    Thread.Sleep(1000);
                }
            }));

            t2.Start();
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();

            stopped = true;
            
            //t.Join();
            // Abort is not su
            //t.Abort();
            t2.Join();
        }
    }
}
