using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndObjectCasting.Models
{
    public enum Weapon
    {
        Claws,
        Fangs,
        Poison,
        ClawsAndFangs,
        ClawsAndPoison,
        ClawsFangsAndPoison,
        FangsAndPoison,
    }
    public class Predator : LivingCreature
    {
        public Weapon Weapons { get; set; }

        public new void GetInfo()
        {
            base.GetInfo();

            Console.WriteLine("--------------------------");
            Console.WriteLine("I am also a Predator");
            Console.WriteLine($"Name: \"{Name}\"");
            Console.WriteLine($"Age: \"{Age}\"");
            Console.WriteLine($"Weapon: \"{Weapons}\"");
            Console.WriteLine("--------------------------");
        }
    }
}
