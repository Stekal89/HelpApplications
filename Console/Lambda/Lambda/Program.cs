using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> personList = new List<Person>();

            personList.Add(new Person(1, "Tom", "Tomson"));
            personList.Add(new Person(2, "Michael", "Johnson"));
            personList.Add(new Person(3, "Tina", "Mayer"));
            personList.Add(new Person(4, "Helene", "Klaus"));
            personList.Add(new Person(5, "Paul", "Zach"));

            string fName = "Tina";

            Console.WriteLine($"Load Person with firstname \"{fName}\"\n");
            Person p = personList.Where(x => x.FirstName == fName).FirstOrDefault();

            if (p != null)
            {
                Console.WriteLine("Founded Person:");
                Console.WriteLine($"ID: {p.ID}");
                Console.WriteLine($"Firstname: {p.FirstName}");
                Console.WriteLine($"Lastname: {p.LastName}");
            }
            else
            {
                Console.WriteLine("Person is NULL!!!");
            }

            Console.ReadKey();
        }
    }
}
