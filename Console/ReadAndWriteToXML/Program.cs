using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ReadAndWriteToXML
{
    class Program
    {
        #region Help-Functions

        /// <summary>
        /// Write console-output in dark-green.
        /// </summary>
        /// <param name="strMsg">Output which should be written to the console</param>
        public static void WriteSuccess(string strMsg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(strMsg);
            Console.ForegroundColor = currentColor;
        }

        /// <summary>
        /// Write console-output in dark-yellow.
        /// </summary>
        /// <param name="strMsg">Output which should be written to the console</param>
        public static void WriteWarning(string strMsg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(strMsg);
            Console.ForegroundColor = currentColor;
        }

        /// <summary>
        /// Write console-output in red.
        /// </summary>
        /// <param name="strMsg">Output which should be written to the console</param>
        public static void WriteError(string strMsg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(strMsg);
            Console.ForegroundColor = currentColor;
        }
        #endregion

        static void Main(string[] args)
        {
            string strPath = @"..\..\TestFile\People.xml";

            LoadPeopleFromXML(strPath);
            LoadPeopleFromXMLUsingLinq(strPath);

            CreateOrAddPersonToXML(@"..\..\TestFile\Test.xml", "Max", "Mustermann", new DateTime(1980,03,22), 41);

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }

        /// <summary>
        /// Loads content of the xml using "System.Xml"
        /// </summary>
        /// <param name="strFilePath">XML-File path</param>
        private static void LoadPeopleFromXML(string strFilePath)
        {
            if (string.IsNullOrEmpty(strFilePath))
            {
                WriteError("\n\nCannot load XML-FIle!\nFilepath is NULL, or empty.");
                return;
            }

            if (!System.IO.File.Exists(strFilePath))
            {
                WriteError($"Cannot load XML-File!" +
                           $"File \"{strFilePath}\" does not exist.");
                return;
            }

            XmlTextReader xmlReader = new XmlTextReader(strFilePath);

            while (xmlReader.Read())
            {
                xmlReader.MoveToElement();
                Console.WriteLine("\n\nXmlTextReader Properties Test");
                Console.WriteLine("===================");
                // Read this element's properties and display them on console  
                Console.WriteLine("Name:" + xmlReader.Name);
                Console.WriteLine("Base URI:" + xmlReader.BaseURI);
                Console.WriteLine("Local Name:" + xmlReader.LocalName);
                Console.WriteLine("Attribute Count:" + xmlReader.AttributeCount.ToString());
                Console.WriteLine("Depth:" + xmlReader.Depth.ToString());
                Console.WriteLine("Line Number:" + xmlReader.LineNumber.ToString());
                Console.WriteLine("Node Type:" + xmlReader.NodeType.ToString());
                Console.WriteLine("Attribute Count:" + xmlReader.Value.ToString());
            }
        }

        /// <summary>
        /// Creates XML-File, or add a node to an existing file if the xml file already exist.
        /// </summary>
        /// <param name="strFilePath">XML-File path</param>
        /// <param name="strFirstName">Firstname</param>
        /// <param name="strLastName">Lastname</param>
        /// <param name="dtBirthDate">Birthdate</param>
        /// <param name="nAge">Age</param>
        private static void CreateOrAddPersonToXML(string strFilePath, string strFirstName, string strLastName, DateTime dtBirthDate, int nAge)
        {
            if (string.IsNullOrEmpty(strFilePath))
            {
                WriteError("\n\nCannot load XML-FIle!\nFilepath is NULL, or empty.");
                return;
            }

            if (!File.Exists(strFilePath))
            {
                // Create new xml-file using "System.Xml"
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = true;
                using (XmlWriter xmlWriter = XmlWriter.Create(strFilePath, xmlWriterSettings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Configuration");

                    xmlWriter.WriteStartElement("People");
                    xmlWriter.WriteStartElement("Person");
                    xmlWriter.WriteElementString("Firstname", strFirstName);
                    xmlWriter.WriteElementString("Lastname", strLastName);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            else
            {
                // Add Node (Person) to xml file using "System.Xml.Linq"
                XDocument xDocument = XDocument.Load(strFilePath);
                XElement root = xDocument.Element("Configuration");

                // The new element will be inserted inside of this node
                IEnumerable<XElement> rows = root.Descendants("People");
                XElement firstRow = rows.First();
                firstRow.Add(new XElement("Person", new XElement("FirstName", strFirstName), new XElement("LastName", strLastName)));
                xDocument.Save(strFilePath);
            }
        }

        /// <summary>
        /// Loads content of the xml using "System.Xml.Linq"
        /// </summary>
        /// <param name="strFilePath">XML-File path</param>
        private static void LoadPeopleFromXMLUsingLinq(string strFilePath)
        {
            if (string.IsNullOrEmpty(strFilePath))
            {
                WriteError("\n\nCannot load XML-FIle!\nFilepath is NULL, or empty.");
                return;
            }

            if (!System.IO.File.Exists(strFilePath))
            {
                WriteError($"Cannot load XML-File!" +
                           $"File \"{strFilePath}\" does not exist.");
                return;
            }

            // Load xml content and parse it into a XDocument object:
            //string xmlString = System.IO.File.ReadAllText(strFilePath);
            //XDocument doc = XDocument.Parse(xmlString);

            // Directly load the xml into a XDocument object:
            XDocument doc = XDocument.Load(strFilePath);

            var people = doc.Root.Descendants().FirstOrDefault(x => x.Name == "People");

            string strFirstname;
            string strLastname;
            DateTime dtBirthDate;
            int nAge;

            if (null != people)
            {
                foreach (var person in people.Elements())
                {
                    if (null != person)
                    {
                        string[] formats = { "yyyyMMdd", "yyyy-MM-dd", "dd-MM-yyyy", "dd.MM.yyyy", "dd.M.yyyy", "d.MM.yyyy", "dd.MM.yy", "dd.M.yy", "d.M.yy", "d.M.yy", "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy"};
                        CultureInfo deDE = new CultureInfo("de-DE");


                        strFirstname = person.Descendants().FirstOrDefault(x => x.Name == "Firstname")?.Value;
                        strLastname = person.Descendants().FirstOrDefault(x => x.Name == "Lastname")?.Value;
                        DateTime.TryParseExact(person.Descendants().FirstOrDefault(x => x.Name == "Birthday")?.Value, formats, null, DateTimeStyles.None, out dtBirthDate);
                        int.TryParse(person.Descendants().FirstOrDefault(x => x.Name == "Age")?.Value, out nAge);

                        WriteSuccess($"\n--------------------------------------------------------------");
                        WriteSuccess($"Type: \"{person.Name}\"");
                        WriteSuccess($"Firstname: \"{strFirstname}\"");
                        WriteSuccess($"Lastname: \"{strLastname}\"");
                        WriteSuccess($"Birthdate: \"{dtBirthDate}\"");
                        WriteSuccess($"Age: \"{nAge}\"");
                        WriteSuccess($"--------------------------------------------------------------\n");

                    }
                }
            }
        }
    }
}
