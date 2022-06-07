using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndObjectCasting.Models
{
    public class Cobra : Predator
    {
        public decimal PoisonStrength { get; set; }
        public int ToothCount { get; set; }

        public new void GetInfo()
        {
            base.GetInfo();

            Console.WriteLine("--------------------------");
            Console.WriteLine("And I am also a Cobra");
            Console.WriteLine($"Name: \"{Name}\"");
            Console.WriteLine($"Age: \"{Age}\"");
            Console.WriteLine($"Weapon: \"{Weapons}\"");
            Console.WriteLine($"PoisonStrength: \"{PoisonStrength}\"");
            Console.WriteLine($"ToothCount: \"{ToothCount}\"");
            Console.WriteLine("--------------------------");
        }
    }
}
