using static System.Net.Mime.MediaTypeNames;

namespace ConvertToBool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string yes = "true";
            string no = "false";

            ConvertStringToBool("true");
            ConvertStringToBool("false");
            ConvertStringToBool("TRUE");
            ConvertStringToBool("FALSE");

            ConvertStringToBool("t");
            ConvertStringToBool("f");
            ConvertStringToBool("T");
            ConvertStringToBool("F");

            ConvertStringToBool("yes");
            ConvertStringToBool("no");
            ConvertStringToBool("YES");
            ConvertStringToBool("NO");
            ConvertStringToBool("y");
            ConvertStringToBool("n");
            ConvertStringToBool("Y");
            ConvertStringToBool("N");

            ConvertStringToBool("ABC");

            ConvertStringToBool(1.ToString());
            ConvertStringToBool(0.ToString());
            ConvertStringToBool(2.ToString());
            ConvertStringToBool((-1).ToString());

            ConvertIntToBoolean(1);
            ConvertIntToBoolean(0);
            ConvertIntToBoolean(2);
            ConvertIntToBoolean(-1);


            ConvertManuallyIntToBoolean(1);
            ConvertManuallyIntToBoolean(0);
            ConvertManuallyIntToBoolean(2);
            ConvertManuallyIntToBoolean(-1);

            Console.WriteLine("\n\nContiue with any key...");
            Console.ReadKey();
        }

        private static void ConvertStringToBool(string text)
        {
            bool converted;
            Console.WriteLine($"\n------------------------------");
            if (bool.TryParse(text, out converted))
            {
                Console.WriteLine($"String {text}: \"{text}\"");
                Console.WriteLine($"String {text}-ToBool: \"{converted}\"");
            }
            else
            {
                Console.WriteLine($"Cannot convert string \"{text}\"");
            }
            Console.WriteLine($"------------------------------");
        }

        private static void ConvertIntToBoolean(int numb)
        {
            Console.WriteLine($"\n------------------------------");
            Console.WriteLine($"Integer \"{numb}\"");
            Console.WriteLine($"Converted: \"{Convert.ToBoolean(numb)}\"");
            Console.WriteLine($"------------------------------");
        }

        private static void ConvertManuallyIntToBoolean(int numb)
        {
            Console.WriteLine($"\n------------------------------");
            Console.WriteLine($"Integer \"{numb}\"");
            Console.WriteLine($"Manually Converted: \"{Convert.ToBoolean(numb)}\"");
            Console.WriteLine($"------------------------------");
        }
    }
}