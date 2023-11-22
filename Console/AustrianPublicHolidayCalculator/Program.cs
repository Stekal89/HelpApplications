using AustrianPublicHolidayCalculator.Models;

namespace AustrianPublicHolidayCalculator
{
    internal class Program
    {
        #region Help-Functions

        /// <summary>
        /// Continue with any key behavior
        /// </summary>
        private static void ContinueWithAnyKey()
        {
            Console.WriteLine("\n\nContinue with any key");
            Console.ReadKey();
        }

        /// <summary>
        /// Writes an Error message to the console
        /// </summary>
        /// <param name="msg"></param>
        private static void WriteError(string msg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{msg}\n");
            Console.ForegroundColor = currentColor;
        }

        /// <summary>
        /// Writes a Warning message to the console
        /// </summary>
        /// <param name="msg"></param>
        private static void WriteWarning(string msg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n{msg}\n");
            Console.ForegroundColor = currentColor;
        }

        /// <summary>
        /// Writes a Success message to the console
        /// </summary>
        /// <param name="msg"></param>
        private static void WriteSuccess(string msg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = currentColor;
        }

        #endregion

        static void Main(string[] args)
        {
            bool exit = false;
            
            do
            {
                ConsoleColor currentColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n######################");
                Console.WriteLine("PUBLIC HOLIDAY CALCULATOR\n");
                Console.WriteLine("For exit the application enter (e)xit");
                Console.WriteLine("######################\n");
                Console.ForegroundColor = currentColor;

                Console.Write($"Please insert year: ");
                string? input = Console.ReadLine();
                Console.WriteLine("");

                if (!string.IsNullOrEmpty(input))
                {
                    if (input.ToLower() != "e" && input.ToLower() != "exit")
                    {
                        if (int.TryParse(input, out int year))
                        {
                            List<PublicHoliday> publicHolidays = PublicHolidayCalculator.GetAustriaPublicHolidays(year, eFederalState.All);

                            foreach (PublicHoliday holiday in publicHolidays)
                            {
                                WriteSuccess($"{holiday.HolidayDate.ToString("dd.MM.yyyy")} -> {holiday.HolidayName}");
                            }
                        }
                        else
                        {
                            WriteError($"Invalid Input!\n" +
                                       $"Cannot parse input into an Integer.\n" +
                                       $"   - Input: \"{input}\"");
                            
                        }
                    }
                    else
                    {
                        Console.WriteLine("Application will be closed.");
                        exit = true;
                    }
                }
                else
                {
                    WriteError("Input is null, or empty!");
                }

                ContinueWithAnyKey();
                Console.Clear();

            } while (!exit);
        }
    }
}