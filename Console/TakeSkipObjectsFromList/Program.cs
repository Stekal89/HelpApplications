using TakeSkipObjectsFromList.Models;

namespace TakeSkipObjectsFromList
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<Person> people = new List<Person>()
			{ 
				new Person()
				{
					FirstName = "Hubert",
					LastName = "Gubicek",
					Age = 13,
				},
				new Person()
				{
					FirstName = "Hubert",
					LastName = "Gubicek",
					Age = 3,
				},
				new Person()
				{
					FirstName = "Jonny",
					LastName = "Johnson",
					Age = 11,
				},
				new Person()
				{
					FirstName = "Michaela",
					LastName = "Ottos",
					Age = 8,
				},
				new Person()
				{
					FirstName = "Jonny",
					LastName = "Jackson",
					Age = 15,
				},
				new Person()
				{
					FirstName = "Claudia",
					LastName = "Regona",
					Age = 17,
				},
				new Person()
				{
					FirstName = "Filippa",
					LastName = "Johnson",
					Age = 5,
				},
				new Person()
				{
					FirstName = "Karl",
					LastName = "Manson",
					Age = 12,
				},
				new Person()
				{
					FirstName = "Samantha",
					LastName = "Jackson",
					Age = 14,
				}
			};

			// Take 5 people of the list, but only 3 are allowed
			List<Person> sortedPeople = GetChildsWhoSlideAfter(people, 5, 3);

			// Take 1 person of the list, where maximal 3 people are allowed
			// So the first 2 people musst be skipped
			List<Person> sortedAndSkippedPeople = GetChildsWhoSlideAfter(people, 1, 3);

			Console.WriteLine($"\n\nContinue with any key...");
			Console.ReadKey();
        }

		static private List<Person> GetChildsWhoSlideAfter(List<Person> people, int toLoad, int maxToLoad)
		{
			List<Person> tmpPeople = null;

			if (toLoad > 0)
			{
				if (toLoad > maxToLoad)
				{
					toLoad = maxToLoad;
				}

				// All people sorted by age (DESCENDING)
				tmpPeople = people.OrderByDescending(ob => ob.Age).ToList();

				if (tmpPeople != null && tmpPeople.Count > 0)
				{
					// Reduce list to the people count of people which should be loaded
					tmpPeople = tmpPeople.Take(maxToLoad).ToList();

					// Skip people which are not neccessary to load
					tmpPeople = tmpPeople.Skip(maxToLoad - toLoad).ToList();

					// Show information of loaded people
					if (tmpPeople != null && tmpPeople.Count > 0)
					{
						Console.WriteLine($"\n################");
						Console.WriteLine($"\"{tmpPeople.Count}\" People will be loaded.");
						Console.WriteLine($"################\n");
						
						foreach (var person in tmpPeople)
						{
							Console.WriteLine($"\n---------------------");
							Console.WriteLine($"Name: \"{person.FirstName} {person.LastName}\"");
							Console.WriteLine($"Age: \"{person.Age}\"");
							Console.WriteLine($"---------------------\n");
						}
					}
				}
			}

			return tmpPeople;
		}
	}
}