using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round
{
    class Program
    {
        static void Main(string[] args)
        {
			string strInput;

			do
            {
				Console.Write("\nPlease enter a number:");
				strInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(strInput))
                {
					if (decimal.TryParse(strInput, out decimal nInputToRound))
					{
						decimal nResult = Math.Round(nInputToRound, MidpointRounding.AwayFromZero);

                        Console.WriteLine($"Result: {nResult}");
					}
					else
					{
						Console.WriteLine($"\nInput was not a number: \"{strInput}\"");
					}
				}
                else
                {
                    Console.WriteLine($"\nInput is NULL or Empty!");
                }

                Console.WriteLine("\n\nContinue with any key...");
				Console.ReadKey();
				Console.Clear();

			} while (strInput.ToUpper() != "E" && strInput.ToUpper() != "EXIT");
        }
	}
}
