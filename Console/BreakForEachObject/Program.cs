using BreakForEachObject.Models;
using System;
using System.Collections.Generic;

namespace BreakForEachObject
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Initialize people
			List<Person> people = new List<Person>()
			{
				new Person("Tom", 22),
				new Person("Michelle", 21),
				new Person("Peter", 32),
				new Person("Victoria", 30),
			};

			// Print person, but abort foreach loop if person older than 30 is recognized
			FindOldPerson(people);

			Console.WriteLine("\n\nContinue with any key...");
			Console.ReadKey();
		}

		/// <summary>
		/// Print person, but abort foreach loop if person older than 30 is recognized
		/// </summary>
		/// <param name="people"></param>
		private static void FindOldPerson(List<Person> people)
		{
			bool oldPersonFound = false;
			people?.ForEach(p =>
			{
				if (p.Age < 30)
				{
					p.PrintInfo();
				}
				else
				{
					// Foreach-Object can be leaved with the return value
					oldPersonFound = true;
					return;
				}
			});

			if (oldPersonFound)
			{
                Console.WriteLine("Old person was detected!");
            }
			else
			{
                Console.WriteLine("No old person was detected");
            }
        }

	}
}
