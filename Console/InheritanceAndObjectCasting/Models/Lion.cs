using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndObjectCasting.Models
{
    public class Lion : Predator
    {
        public decimal HairLength { get; set; }

        public new void GetInfo()
        {
            base.GetInfo();

            Console.WriteLine("--------------------------");
            Console.WriteLine("And I am also a Lion");
            Console.WriteLine($"Name: \"{Name}\"");
            Console.WriteLine($"Age: \"{Age}\"");
            Console.WriteLine($"Weapon: \"{Weapons}\"");
            Console.WriteLine($"HairLength: \"{HairLength}\"");
            Console.WriteLine("--------------------------");
        }
    }
}
