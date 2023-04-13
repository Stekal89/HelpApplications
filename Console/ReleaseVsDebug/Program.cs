namespace ReleaseVsDebug
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string output = "\n--------------------\n" +
                            "#if DEBUG\n" +
                            "#else\n" +
                            "#endif\n";

#if DEBUG
            Console.WriteLine(output);
            Console.WriteLine($"It is a Debug-Build");
#else
            Console.WriteLine(output);
            Console.WriteLine($"It is a Release-Build");
#endif
            Console.WriteLine("--------------------\n");

            output = "\n--------------------\n" +
                     "#if RELEASE\n" +
                     "#else\n" +
                     "#endif\n";

#if RELEASE
            Console.WriteLine(output);
            Console.WriteLine($"It is a Release-Build");
#else
            Console.WriteLine(output);
            Console.WriteLine($"It is a Debug-Build");
#endif
            Console.WriteLine("--------------------^\n");

            output = "\n--------------------\n" +
                     "#if !DEBUG\n" +
                     "#else\n" +
                     "#endif\n";

#if !DEBUG
            Console.WriteLine(output);
            Console.WriteLine($"It is a Release-Build");
#else
            Console.WriteLine(output);
            Console.WriteLine($"It is a Debug-Build");
#endif
            Console.WriteLine("--------------------^\n");

            Console.WriteLine($"\n\nContinue with any key...");
            Console.ReadKey();
        }
    }
}