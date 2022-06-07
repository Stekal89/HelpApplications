using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndObjectCasting.Models
{
    public enum Gender
    {
        Male,
        Female,
        Others
    }
    public class Human : LivingCreature
    {
        public Gender Gender { get; set; }

        public new void GetInfo()
        {
            base.GetInfo();

            Console.WriteLine("--------------------------");
            Console.WriteLine("I am also a Human");
            Console.WriteLine($"Name: \"{Name}\"");
            Console.WriteLine($"Age: \"{Age}\"");
            Console.WriteLine($"Gender: \"{Gender}\"");
            Console.WriteLine("--------------------------");
        }
    }
}
