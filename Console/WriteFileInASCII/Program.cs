using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WriteFileInASCII
{
    class Program
    {
        static void Main(string[] args)
        {
            string strDirectory = @"C:\temp\ASCIITest";

            // Check if path exist:
            Console.WriteLine($"Check if directory \"{strDirectory}\" exist...");
            if (!System.IO.Directory.Exists(strDirectory))
            {
                // Create directory, if it does not exist:
                Console.WriteLine($"\nDirectory \"{strDirectory}\" does not exist!");
                Console.WriteLine($"Create directory \"{strDirectory}\"...");
                System.IO.Directory.CreateDirectory(strDirectory);
            }

            CreateFile(Path.Combine(strDirectory, "UTF7Test.txt"), Encoding.UTF7);
            CreateFile(Path.Combine(strDirectory, "UTF7Test.csv"), Encoding.UTF7);
            CreateFile(Path.Combine(strDirectory, "UTF8Test.txt"), Encoding.UTF8);
            CreateFile(Path.Combine(strDirectory, "UTF8Test.csv"), Encoding.UTF8);
            CreateFile(Path.Combine(strDirectory, "UTF32Test.txt"), Encoding.UTF32);
            CreateFile(Path.Combine(strDirectory, "UTF32Test.csv"), Encoding.UTF32);
            CreateFile(Path.Combine(strDirectory, "UnicodeTest.txt"), Encoding.Unicode);
            CreateFile(Path.Combine(strDirectory, "UnicodeTest.csv"), Encoding.Unicode);
            CreateFile(Path.Combine(strDirectory, "DefualtASCIITest.txt"), Encoding.ASCII);
            CreateFile(Path.Combine(strDirectory, "DefaultASCIITest.csv"), Encoding.ASCII);
            CreateFile(Path.Combine(strDirectory, "GermanASCIITest.txt"), Encoding.GetEncoding(437));
            CreateFile(Path.Combine(strDirectory, "GermanASCIITest.csv"), Encoding.GetEncoding(437));

            //Thread.Sleep(new TimeSpan(0, 0, 5));
            ShowEncoding(strDirectory, "*.txt");
            ShowEncoding(strDirectory, "*.csv");

            //string sample = "ABC ÖäÜ⌐";
            //Encoding cpEN = Encoding.GetEncoding(437);
            //Byte[] byteCodePageEnglish = cpEN.GetBytes(sample);
            //Console.WriteLine("Dekodierte Zeichenkette: {0}", cpEN.GetString(byteCodePageEnglish));
            //Encoding cptk = Encoding.GetEncoding(437);
            //Console.WriteLine("Dekodierte Zeichenkette: {0}", cptk.GetString(byteCodePageEnglish));

            Console.WriteLine("\n\n\nContinue with any key...");
            Console.ReadKey();
        }

        /// <summary>
        /// Create Dummy-Files
        /// </summary>
        /// <param name="strFilePath">FilePath</param>
        /// <param name="encoding">Encoding-Object</param>
        public static void CreateFile(string strFilePath, Encoding encoding)
        {
            // Create file
            Console.WriteLine($"\nCreate file \"{strFilePath}\"");
            using (System.IO.FileStream fs = System.IO.File.Create(strFilePath))
            {
                // Verify if the file was created successfully:
                Console.WriteLine($"Verify if file \"{strFilePath}\" was successfully created...");
                if (System.IO.File.Exists(strFilePath))
                {
                    // File was created:
                    Console.WriteLine($"File \"{strFilePath}\" was created successfully.");

                    //StreamWriter sw1 = new StreamWriter(strFilePath, true, Encoding.ASCII);
                    using (StreamWriter sw = new StreamWriter(fs, encoding))
                    {
                        Console.WriteLine("\nWrite File-Content...");
                        for (int i = 0; i < 10; i++)
                        {
                            sw.WriteLine($"ASCII line: \"{i}\" -> AEIOU Ä Ö Ü");
                        }
                    }
                }
                else
                {
                    // File was not created:
                    Console.WriteLine($"File \"{strFilePath}\" was not created!");
                }
            }
        }

        /// <summary>
        /// Shows the Encoding of all files in a specified directory. (Only files with the extenstions will be loaded)
        /// </summary>
        /// <param name="strDirectory">Directory</param>
        /// <param name="strExtension">File-Extensiton</param>
        public static void ShowEncoding(string strDirectory, string strExtension)
        {
            Console.WriteLine("\n\n");

            foreach (string file in Directory.EnumerateFiles(strDirectory, strExtension))
            {
                // Verify if the file was written in ASCII Encoding:
                Encoding encoding = GetEncoding(file);

                Console.WriteLine("######################################################################################################");
                Console.WriteLine($"File: \"{file}\"");

                if (encoding == Encoding.ASCII || encoding == Encoding.GetEncoding(437))
                {
                    Console.WriteLine($"YES: GetEncoding: \"{encoding.EncodingName}/{encoding}\"");
                }
                else
                {
                    Console.WriteLine($"NO: GetEncoding: \"{encoding.EncodingName}/{encoding}\"");
                }

                Encoding encoding2 = GetEncoding2(file);
                Encoding test = Encoding.GetEncoding(437);
                if (encoding2 == Encoding.ASCII || encoding2 == Encoding.GetEncoding(437))
                {
                    Console.WriteLine($"YES: GetEncoding2: \"{encoding2.EncodingName}/{encoding2}\"");
                }
                else
                {
                    Console.WriteLine($"NO: GetEncoding2: \"{encoding2.EncodingName}/{encoding2}\"");
                }

                Console.WriteLine("######################################################################################################\n");
            }
        }

        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0) return Encoding.UTF32; //UTF-32LE
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return new UTF32Encoding(true, true);  //UTF-32BE

            // We actually have no idea what the encoding is if we reach this point, so
            // you may wish to return null instead of defaulting to ASCII
            return Encoding.ASCII;
        }

        /// <summary>
        /// Get File's Encoding
        /// </summary>
        /// <param name="filename">The path to the file</param>
        private static Encoding GetEncoding2(string filename)
        {
            // This is a direct quote from MSDN:  
            // The CurrentEncoding value can be different after the first
            // call to any Read method of StreamReader, since encoding
            // autodetection is not done until the first call to a Read method.

            using (var reader = new StreamReader(filename, Encoding.Default, true))
            {
                if (reader.Peek() >= 0) // you need this!
                    reader.Read();

                return reader.CurrentEncoding;
            }
        }
    }
}
