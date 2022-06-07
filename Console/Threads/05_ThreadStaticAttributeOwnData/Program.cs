using System;
using System.Threading;

namespace _05_ThreadStaticAttributeOwnData
{
    class Program
    {
        [ThreadStatic]
        public static int field;
        public static int field2;

        public static bool finished1 = false;
        public static bool finished2 = false;

        /// <summary>
        /// Uses for both threads his own value, so the value bocomes 10 at the end.
        /// </summary>
        public static void ThreadMethod()
        {
            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    field++;
                    Console.WriteLine($"Thread A: \"{field}\"");
                }
                finished1 = true;
            }).Start();

            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    field++;
                    Console.WriteLine($"Thread B: \"{field}\"");
                }
            }).Start();
        }

        /// <summary>
        /// Both threads are using the same value, so the value becomes 20 at the end.
        /// </summary>
        public static void ThreadMethod2()
        {
            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    field2++;
                    Console.WriteLine($"Thread C: \"{field2}\"");
                }
            }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    field2++;
                    Console.WriteLine($"Thread D: \"{field2}\"");
                }
            }).Start();
        }


        static void Main(string[] args)
        {
            ThreadMethod();
            while (!finished1 && !finished2)
            {
                Thread.Sleep(new TimeSpan(0,0,0,5));
            }
            Console.WriteLine("\n");
            ThreadMethod2();

            Console.ReadKey();
        }
    }
}
