using ForEachVsForeachObject.Models;
using System.Threading.Channels;

namespace ForEachVsForeachObject
{
	internal class Program
	{
		static Random random = new Random();

		static void Main(string[] args)
		{
			#region Filled List

			List<Person> people = new List<Person>()
			{
				new Person("Jonny Jonson", 23),
				new Person("Jimmy Jackson", 32),
				new Person("Angela Smith", 25)
			};

			// Normal Implementation:
			Console.WriteLine("Filled List (Normal Implementation):");
			ModifyAgesInForEach(people);

			// ForEach Object Function
			Console.WriteLine("Filled List (ForEach Object Function):");
			Console.WriteLine("\n---------------------------------------");
			people.ForEach(person =>
			{
				int newAge = random.Next(5, 40);

				Console.WriteLine($"Change Age of \"{person.Name}\" from \"{person.Age}\" to \"{newAge}\"");

				person.Age = newAge;
			});
			Console.WriteLine("---------------------------------------\n");

			#endregion // Filled List

			#region Empty List

			people = new List<Person>();

			// Normal Implementation:
			Console.WriteLine("Empty List (Normal Implementation):");
			ModifyAgesInForEach(people);

			// ForEach Object
			Console.WriteLine("Empty List (ForEach Object Function):");
			Console.WriteLine("\n---------------------------------------");
			people?.ForEach(person =>
			{
				int newAge = random.Next(5, 40);

				Console.WriteLine($"Change Age of \"{person.Name}\" from \"{person.Age}\" to \"{newAge}\"");

				person.Age = newAge;
			});
			Console.WriteLine("---------------------------------------\n");

			#endregion // Empty List


			#region Null List

			people = null;

			// Normal Implementation:
			Console.WriteLine("Null List (Normal Implementation):");
			ModifyAgesInForEach(people);

			// ForEach Object
			Console.WriteLine("NULL List (ForEach Object Function):");
			Console.WriteLine("\n---------------------------------------");

			// ####################
			// Using the QuestionMark, makes it possible to use the ForEach Object Functionality at this way!!!
			// ####################
			people?.ForEach(person =>
			{
				int newAge = random.Next(5, 40);

				Console.WriteLine($"Change Age of \"{person.Name}\" from \"{person.Age}\" to \"{newAge}\"");

				person.Age = newAge;
			});
			Console.WriteLine("---------------------------------------\n");

			#endregion // Null List

			Console.WriteLine($"\n\nContinue with any key...");
			Console.ReadKey();
        }

		private static void ModifyAgesInForEach(List<Person> people)
		{
			if (people != null && people.Count > 0)
			{
                Console.WriteLine("\n---------------------------------------");
                foreach (var item in people)
				{
					int newAge = random.Next(5, 40);

                    Console.WriteLine($"Change Age of \"{item.Name}\" from \"{item.Age}\" to \"{newAge}\"");

                    item.Age = newAge;
				}
				Console.WriteLine("---------------------------------------\n");
			}
		}
	}
}
