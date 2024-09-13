using System;

namespace BreakForEachObject.Models
{
	internal class Person
	{
        #region Properties

        /// <summary>
        /// Name of person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Age of person
        /// </summary>
		public int Age { get; set; }

		#endregion // Properties

		#region Constructors

		/// <summary>
		/// Default-Constructor
		/// </summary>
		public Person()
        {
        
        }

        /// <summary>
        /// Full-Constructor
        /// </summary>
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

		#endregion // Constructors

		#region Functions

        public void PrintInfo()
        {
			Console.WriteLine($"\n------------------------");
            Console.WriteLine($"Name: \"{Name}\"");
			Console.WriteLine($"Age: \"{Age}\"");
			Console.WriteLine($"------------------------\n");
		}

		#endregion // Functions

	}
}
