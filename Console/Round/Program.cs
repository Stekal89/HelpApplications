using System;
using System.Linq;

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
                        //decimal nResult = Math.Round(nInputToRound, 2, MidpointRounding.AwayFromZero);
                        decimal nResult = RoundNumber(nInputToRound, 2);

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

        /// <summary>
        /// Round for each digit from right to left
        /// </summary>
        /// <param name="number">Number which should be round</param>
        /// <param name="decimals">digits after the comma</param>
        /// <returns>Rounded Number</returns>
        public static decimal RoundNumber(decimal number, int decimals)
        {
            decimal result = number;

            if (Math.Truncate(number) != number)
            {
                int commaCount = BitConverter.GetBytes(decimal.GetBits(number)[3])[2];
                int count = number.ToString().Count() - 1;

                int commaCountDecimals =  commaCount - decimals;
                int diff = count - commaCount;

                if (commaCountDecimals > 0 && diff > 0)
                {
                    result = number;

                    for (int i = 0; i < commaCountDecimals; i++)
                    {
                        result = Math.Round(result, commaCount - 1, MidpointRounding.AwayFromZero);
                        commaCount--;
                        Console.WriteLine(result);
                    }
                }
            }
            return result;
        }
    }
}
