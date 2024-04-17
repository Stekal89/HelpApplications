using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEachVsForeachObject.Models
{
	public class Person
	{
        /// <summary>
        /// Full-Name
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Default-Constructor
        /// </summary>
        public Person()
        {
                
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="name">Name of Person</param>
        /// <param name="age">Age of Person</param>
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
