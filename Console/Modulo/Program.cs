using System.Globalization;

namespace Modulo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowModCalculation(12, 1);
            ShowModCalculation(12, 3);
            ShowModCalculation(13, 3);
            ShowModCalculation(14, 3);
            ShowModCalculation(15, 3);
            ShowModCalculation(16, 3);
            ShowModCalculation(17, 3);
            ShowModCalculation(13, 2);
            ShowModCalculation(14, 3);

            Console.WriteLine("\r\nContinue with any key...");
            Console.ReadKey();
        }

        static void ShowModCalculation(int numb, int modNumb)
        {
            Console.WriteLine($"\n--------------------");
            Console.WriteLine($"{numb} / {modNumb} = {((decimal)numb / (decimal)modNumb)}");
            Console.WriteLine($"{numb} % {modNumb} = {numb % modNumb}");
            Console.WriteLine($"--------------------\n");
        }
    }
}