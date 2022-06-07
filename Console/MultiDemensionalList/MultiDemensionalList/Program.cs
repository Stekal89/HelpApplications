using System;
using System.Collections.Generic;

namespace MultiDemensionalList
{
    public struct NumbDescription
    {
        public NumbDescription(string strNumb, int nNumb)
        {
            StringData = strNumb;
            IntegerData = nNumb;
        }
        public string StringData { get; set; }
        public int IntegerData { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<NumbDescription> numbDescriptions = new List<NumbDescription>();

            // 2 Possibilities to create a new "NumbDescription" Object:
            // First:
            numbDescriptions.Add(new NumbDescription()
            {
                StringData = "One",
                IntegerData = 1,
            });

            // Second:
            numbDescriptions.Add(new NumbDescription("Two", 2));
            numbDescriptions.Add(new NumbDescription("Three", 3));
            numbDescriptions.Add(new NumbDescription("Four", 4));
            numbDescriptions.Add(new NumbDescription("Five", 5));
            numbDescriptions.Add(new NumbDescription("Six", 6));
            numbDescriptions.Add(new NumbDescription("Seven", 7));
            numbDescriptions.Add(new NumbDescription("Eight", 8));
            numbDescriptions.Add(new NumbDescription("Nine", 9));
            numbDescriptions.Add(new NumbDescription("Ten", 10));

            Console.WriteLine("Multidemsional Lists using a struct:\n");

            foreach (var item in numbDescriptions)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine($"Text: \"{item.StringData}\"");
                Console.WriteLine($"Numb: \"{item.IntegerData}\"");
                Console.WriteLine("-------------------------\n");
            }


            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }
    }
}
