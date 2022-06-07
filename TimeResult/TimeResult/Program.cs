using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TimeResult
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = new DateTime(2017,6,4,12,00,00);
            DateTime end = new DateTime(2017,6,4,12,30,00);
            

            Console.WriteLine($"Start-time: { start.ToString("dd.MM.yyyy-HH:MM") }");
            Console.WriteLine($"End-time: { end.ToString("dd.mmm.yyyy-HH:MM") }");

            TimeSpan elapsedTime = end - start;

            Console.WriteLine($"Elapsed time: { elapsedTime.ToString(@"hh\:mm") }");

            Console.ReadKey();


        }
    }
}