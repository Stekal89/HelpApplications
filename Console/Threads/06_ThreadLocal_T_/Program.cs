using System;
using System.Threading;

namespace _06_ThreadLocal_T_
{
    class Program
    {
        // You can use the Thread.CurrentThread class to ask for information about the 
        // thread that’s executing. This is called the thread’s execution context.
        
        // This way the new thread has the same privileges as the parent thread.
        // This copying of data does cost some resources, however. If you don’t need this
        // data, you can disable this behavior by using the ExecutionContext.SuppressFlow method.
        public static ThreadLocal<int> field = new ThreadLocal<int>(() =>
        {
            return Thread.CurrentThread.ManagedThreadId;
        });

        static void Main(string[] args)
        {
            new Thread(() =>
            {
                for (int i = 0; i < field.Value; i++)
                {
                    Console.WriteLine($"Thread A: \"{i}\"");
                }
            }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < field.Value; i++)
                {
                    Console.WriteLine($"Thread B: \"{i}\"");
                }
            }).Start();


            Console.ReadKey();
        }
    }
}
