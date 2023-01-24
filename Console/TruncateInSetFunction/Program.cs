using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruncateInSetFunction.Models;

namespace TruncateInSetFunction
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Example is inside the Set-Functionality of the Person class

            Person person = new Person()
            {
                FirstName = "Markus",
                LastName = "Thomson"
            };

            person.ShowInformation();


            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }

        public static string TruncateText(this string strValue, int nMaxLength)
        {
            if (string.IsNullOrEmpty(strValue)) return strValue;
            return strValue.Length <= nMaxLength ? strValue : strValue.Substring(0, nMaxLength);
        }
    }
}
