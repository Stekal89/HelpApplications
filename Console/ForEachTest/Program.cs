using ForEachTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEachTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>()
            {
                new Person() { FirstName = "Tom",      LastName = "Williams",   Age = 20 },
                new Person() { FirstName = "Michael",  LastName = "Zuckerberg", Age = 15 },
                new Person() { FirstName = "Jessica",  LastName = "Gates",      Age = 31 },
                new Person() { FirstName = "Samantha", LastName = "Musk",       Age = 45 }
            };

            Console.WriteLine("############################################################");
            Console.WriteLine("Before:");
            foreach (var person in people)
            {
                PrintInfo(person);
            }

            Console.WriteLine("############################################################\n\n");

            foreach (var personToChange in people)
            {
                if (personToChange.FirstName == "Michael")
                {
                    personToChange.FirstName = "Changed Firstname";
                }

                Console.WriteLine("Inside the foreach loop:");
                PrintInfo(personToChange);
            }

            Console.WriteLine("############################################################\n\n");

            foreach (var changedPerson in people)
            {
                Console.WriteLine("After the foreach loop:");
                PrintInfo(changedPerson);
            }

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }

        private static void PrintInfo(Person person)
        {
            Console.WriteLine($"Firstname: \"{person.FirstName}\"");
            Console.WriteLine($"Lastname:  \"{person.LastName}\"");
            Console.WriteLine($"Age:       \"{person.Age}\"\n");
        }
    }
}
