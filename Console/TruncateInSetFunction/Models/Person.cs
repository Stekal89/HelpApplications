using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TruncateInSetFunction.Models
{
    public class Person
    {
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    firstName = Program.TruncateText(value, 4);
                }
                else
                {
                    firstName = value;
                }
            }
        }


        public string LastName { get; set; }

        /// <summary>
        /// Shows the infromation of the Properties
        /// </summary>
        public void ShowInformation()
        {
            Console.WriteLine($"---------------------------------");
            Console.WriteLine($"--- Person ---");
            Console.WriteLine($"FirstName: \"{FirstName}\"");
            Console.WriteLine($"Length: \"{FirstName.Length}\"");
            Console.WriteLine($"FirstName: \"{LastName}\"");
            Console.WriteLine($"Length: \"{LastName.Length}\"");
            Console.WriteLine($"---------------------------------\n");
        }
    }
}
