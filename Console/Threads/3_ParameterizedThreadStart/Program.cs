using System;
using System.Threading;

namespace _3_ParameterizedThreadStart
{
    class Program
    {
        public static void ThreadMethod(object o)
        {
            for (int i = 0; i < (int)o; i++)
            {
                Console.WriteLine($"ThreadProc: \"{i}\"");
                Thread.Sleep(0);
            }
        }

        public static void ThreadMethod2(int numb, string text)
        {
            for (int i = 0; i < numb; i++)
            {
                Console.WriteLine($"ThreadProc_2: \"{numb}\"");
                Console.WriteLine(text);
                Thread.Sleep(0);
            }
        }

        static void Main(string[] args)
        {
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod));
            t.Start(5);
            //t.Join();

            // This will not work, because only one parameter type=object is possible.
            //Thread t2 = new Thread(new ParameterizedThreadStart(ThreadMethod2));
            //t2.Start(10, "Some Text");

            t.Join();

            Console.WriteLine("\n\nContinue with any key...\n");
            Console.ReadKey();
        }
    }
}
