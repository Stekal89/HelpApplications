using InheritanceAndObjectCasting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndObjectCasting
{
    class Program
    {
        static void Main(string[] args)
        {
            // List of Main-Objects, to store all of the Sub-Category Objects in one list
            List<LivingCreature> livingCreatures = new List<LivingCreature>() 
            {
                new LivingCreature()
                {
                    Name = "Peppi",
                    Age = 12
                },
                new Human()
                {
                    Name = "Michael Willson",
                    Age = 34,
                    Gender = Gender.Male
                },
                new Lion()
                {
                    Name = "Simba",
                    Age = 6,
                    Weapons = Weapon.ClawsAndFangs,
                    HairLength = 12.5m
                },
                new Predator()
                {
                    Name = "Malibu",
                    Age = 15,
                    Weapons = Weapon.ClawsFangsAndPoison
                },
                new Tiger()
                {
                    Name = "Shir Khan",
                    Age = 14,
                    Weapons = Weapon.ClawsAndFangs,
                    Stripes = 24
                },
                new Human()
                { 
                    Name = "Jenna",
                    Age = 25,
                    Gender = Gender.Female
                },
                new Cobra()
                {
                    Name = "Cobi",
                    Age = 3,
                    Weapons = Weapon.FangsAndPoison,
                    PoisonStrength = 12.9m,
                    ToothCount = 2
                }
            };

            foreach (var creature in livingCreatures)
            {
                Console.WriteLine("\n#########################################################");
                if (creature is Human actHuman)
                {
                    actHuman.GetInfo();
                }
                else if (creature is Lion actLion)
                {
                    actLion.GetInfo();
                }
                else if (creature is Tiger actTiger)
                {
                    actTiger.GetInfo();
                }
                else if (creature is Cobra actCobra)
                {
                    actCobra.GetInfo();
                }
                else if (creature is Predator actPredator)
                {
                    actPredator.GetInfo();
                }
                else if (creature is LivingCreature actCreature)
                {
                    actCreature.GetInfo();
                }

                Console.WriteLine("\n#########################################################\n");
            }

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }
    }
}
