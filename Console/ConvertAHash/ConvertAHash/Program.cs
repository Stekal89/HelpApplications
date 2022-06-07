using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConvertAHash
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string text = GetHash("123User!");

            Console.WriteLine(text);
            Console.ReadKey();
        }

        /// <summary>
        /// Best way!!!
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetHash(string input)
        {
            return string.Join("", (new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input))).Select(x => x.ToString("X2")).ToArray());
        }
    }
}
