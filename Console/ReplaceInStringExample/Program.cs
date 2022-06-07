using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceInStringExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ReplaceString("ABCDEFGHI", "BC", null);
            // Output: "ADEFGHI"

            ReplaceString("abcdefghi", "BC", null);
            // Output: "abcdefghi"

            ReplaceString("ABCDEFGHI", "BG", null);
            // Output: "ABCDEFGHI"

            // !!! So the Replace will work only exactly!!!

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }

        private static void ReplaceString(string strToReplace, string strSearch, string strReplaceValue)
        {
            Console.WriteLine("\n------------------------------");
            Console.WriteLine($"Text:\"{strToReplace}\"");
            Console.WriteLine($"Search for: \"{strSearch}\"");
            Console.WriteLine($"Replace with: \"{strReplaceValue}\"");
            Console.WriteLine($"New Text: \"{strToReplace.Replace(strSearch, strReplaceValue)}\"");
            Console.WriteLine("------------------------------\n");
        }
    }
}
