using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndObjectCasting.Models
{
    public class LivingCreature
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public void GetInfo()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("I am a LivingCreature");
            Console.WriteLine($"Name: \"{Name}\"");
            Console.WriteLine($"Age: \"{Age}\"");
            Console.WriteLine("--------------------------");
        }
    }
}
