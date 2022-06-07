using Connection_XAMPP_MySQL.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection_XAMPP_MySQL
{
    class Program
    {
        #region Color-Functions

        private static void Blue()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public static void Gray()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void Cyan()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        private static void Margenta()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }

        private static void Green()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private static void Red()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        private static void Yellow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        private static void DarkYellow()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        private static void DarkBlue()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        }

        #endregion
        
        static void Main(string[] args)
        {
            if (XAMPP.StartApache())
                Console.WriteLine("\n\"Apache\" service started successfully.");
            else
                Console.WriteLine("\nSomething happens during startup of \"Apache\" service!");
            if (XAMPP.StartMySQL())
                Console.WriteLine("\n\"MySQL\" service started successfully");
             else
                Console.WriteLine("\nSomething happens during startup of \"MySQL\" service!");

            PressKeyToContinue();
            
            MainMenu();
            
            XAMPP.StopService(XAMPP.mspn);
            XAMPP.StopService(XAMPP.apn);
        }

        #region Help-Functions

        /// <summary>
        /// Press any key to continue...
        /// </summary>
        public static void PressKeyToContinue()
        {
            Gray();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Default case for the menus
        /// </summary>
        /// <param name="input"></param>
        private static void DefaultCase(ConsoleKeyInfo input)
        {
            DarkYellow();
            Console.Clear();
            Console.WriteLine("\n\nIncorrect input....");
            Console.WriteLine($"You pressed the key: \"{input.Key}\"");
            Gray();
            PressKeyToContinue();
        }

        #endregion

        #region Application Functions

        #region Main Menu
        
        /// <summary>
        /// Main-Menu of this console Application, you can choose  a option by pressing the number, you can leave the application by pressing the ESC key
        /// </summary>
        private static void MainMenu()
        {
            ConsoleKeyInfo input;
            do
            {
                Blue();
                Console.WriteLine("\n\n\t# # # M A I N - M E N U # # #");
                Console.WriteLine("Choose by pressing the number key of the option.");
                Console.WriteLine("1) List all \"Persons\".");
                Console.WriteLine("2) List \"Persons\" by name.");
                Console.WriteLine("3) List \"persons\" by id.");
                Console.WriteLine("4) Create new \"Peson\".");
                Console.WriteLine("5) Modify existing \"persons\".");
                Gray();
                Console.WriteLine("\"ESC\" to close the application");
                input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        ListAllPersons();
                        PressKeyToContinue();
                        break;
                    case ConsoleKey.D1:
                        Console.Clear();
                        ListAllPersons();
                        PressKeyToContinue();
                        break;
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        ListPersonsByName();
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        ListPersonsByName();
                        break;
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        ListPersonById();
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        ListPersonById();
                        break;
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        CreateNewPerson();
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        CreateNewPerson();
                        break;
                    case ConsoleKey.NumPad5:
                        Console.Clear();
                        ModifyExistingPerson();
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        ModifyExistingPerson();
                        break;
                    case ConsoleKey.Escape:
                        break;
                    default:
                        DefaultCase(input);
                        break;
                }
            } while (input.Key != ConsoleKey.Escape);            
        }

        #endregion

        #region List All Persons

        /// <summary>
        /// Loads all persons from the database and displays the list on the Console window.
        /// </summary>
        private static void ListAllPersons()
        {
            try
            {
                List<Person> persons = new List<Person>();
                persons = Database.GetAllPersons();

                if (persons != null)
                {
                    Console.WriteLine("\nID \t | Firstname \t | Lastname");
                    Console.WriteLine("_________________________________________");
                    foreach (Person person in persons)
                    {
                        Console.WriteLine($"{person.Id} \t | {person.FirstName} \t | {person.LastName}");
                    }

                    //PressKeyToContinue();
                }
                else
                    Console.WriteLine("List of Persons is null!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError during \"ListAllPerson\"...");
                Console.WriteLine(ex);
                PressKeyToContinue();
                throw;
            }

        }

        #endregion

        #region List Persons by Name(s)

        /// <summary>
        /// Menu for List persons by name, it is possible to search and list persons by:
        /// - Firstname
        /// - Lastname
        /// - First- and Lastname
        /// </summary>
        private static void ListPersonsByName()
        {
            ConsoleKeyInfo input;

            do
            {
                Cyan();
                Console.WriteLine("\n\n\t# # # L I S T   P E R S O N S   B Y   N A M E - M E N U # # #");
                Console.WriteLine("Choose by pressing the number key of the option.");
                Console.WriteLine("1) Search for \"Persons\" by \"Firstname\".");
                Console.WriteLine("2) Search for \"Persons\" by \"Lastname\".");
                Console.WriteLine("3) Search for \"Persons\" by \"First- and Lastname\".");
                Gray();
                Console.WriteLine("\"ESC\" or \"Backspace\" to switch back to the \"Main-Menu\"");
                input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        ListPersonByFirstName();
                        break;
                    case ConsoleKey.D1:
                        Console.Clear();
                        ListPersonByFirstName();
                        break;
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        ListPersonByLastName();
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        ListPersonByLastName();
                        break;
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        ListPersonByFirstAndLastName();
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        ListPersonByFirstAndLastName();
                        break;
                    case ConsoleKey.Backspace:
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        break;
                    default:
                        DefaultCase(input);
                        break;
                }
            } while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.Backspace);
        }

        /// <summary>
        /// Loads persons using the "Firstname" from the database and displays the list on the Console window.
        /// </summary>
        private static void ListPersonByFirstName()
        {
            Margenta();
            Console.WriteLine("\n\n\t# # # L I S T   P E O P L E   B Y   F I R S T N A M E - M E N U # # #");
            Console.Write("Enter the \"Firstname\" for which you want to search: ");
            Gray();
            string firstName = Console.ReadLine();

            try
            {
                List<Person> persons = new List<Person>();
                persons = Database.GetAllPersonsByFirstName(firstName);

                if (persons != null)
                {
                    Console.WriteLine("\nID \t | Firstname \t | Lastname");
                    Console.WriteLine("_________________________________________");
                    foreach (Person person in persons)
                    {
                        Console.WriteLine($"{person.Id} \t | {person.FirstName} \t | {person.LastName}");
                    }

                    PressKeyToContinue();
                }
                else
                {
                    Yellow();
                    Console.WriteLine($"Cannot find persons/persons with the firstname: \"{firstName}\"");
                    PressKeyToContinue();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError during \"ListAllPerson\"...");
                Console.WriteLine(ex);
                PressKeyToContinue();
                throw;
            }
        }

        /// <summary>
        /// Loads persons using the "Lastname" from the database and displays the list on the Console window.
        /// </summary>
        private static void ListPersonByLastName()
        {
            Margenta();
            Console.WriteLine("\n\n\t# # # L I S T   P E O P L E   B Y   L A S T N A M E - M E N U # # #");
            Console.Write("Enter the \"Lastname\" for which you want to search: ");
            Gray();
            string lastName = Console.ReadLine();

            try
            {
                List<Person> persons = new List<Person>();
                persons = Database.GetAlPersonsByLastName(lastName);

                if (persons != null)
                {
                    Console.WriteLine("\nID \t | Firstname \t | Lastname");
                    Console.WriteLine("_________________________________________");
                    foreach (Person person in persons)
                    {
                        Console.WriteLine($"{person.Id} \t | {person.FirstName} \t | {person.LastName}");
                    }

                    PressKeyToContinue();
                }
                else
                {
                    Yellow();
                    Console.WriteLine($"Cannot find persons/persons with the lastname: \"{lastName}\"");
                    PressKeyToContinue();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError during \"ListAllPerson\"...");
                Console.WriteLine(ex);
                PressKeyToContinue();
                throw;
            }
        }

        /// <summary>
        /// Loads persons using the "Firstname" and the "Lastname" from the database and displays the list on the Console window.
        /// </summary>
        private static void ListPersonByFirstAndLastName()
        {
            Margenta();
            Console.WriteLine("\n\n\t# # # L I S T   P E O P L E   B Y   F I R S T -   A N D   L A S T N A M E - M E N U # # #");
            Console.Write("Enter the \"Firstname\" for which you want to search: ");
            Gray();
            Gray();
            string firstName = Console.ReadLine();
            Margenta();
            Console.Write("Enter the \"Lastname\" for which you want to search: ");
            Gray();
            string lastName = Console.ReadLine();

            try
            {
                List<Person> persons = new List<Person>();
                persons = Database.GetAllPersonsByFirstAndLastName(firstName, lastName);

                if (persons != null)
                {
                    Console.WriteLine("\nID \t | Firstname \t | Lastname");
                    Console.WriteLine("_________________________________________");
                    foreach (Person person in persons)
                    {
                        Console.WriteLine($"{person.Id} \t | {person.FirstName} \t | {person.LastName}");
                    }

                    PressKeyToContinue();
                }
                else
                {
                    Yellow();
                    Console.WriteLine($"Cannot find persons/persons with the firstname: \"{firstName}\"");
                    Console.WriteLine($"and the lastname: \"{lastName}\"");
                    PressKeyToContinue();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError during \"ListAllPerson\"...");
                Console.WriteLine(ex);
                PressKeyToContinue();
                throw;
            }
        }

        #endregion

        #region List Persons by ID

        /// <summary>
        /// Loads a persons using the unique id as console input from the database and displays the persons on the Console window.
        /// </summary>
        private static void ListPersonById()
        {
            bool exit = false;
            do
            {
                DarkBlue();
                Console.WriteLine("\n\n\t# # # L I S T   P E R S O N   B Y   I D - M E N U # # #");
                Console.WriteLine("To leave the menu write (e)xit into the console window and press the enter key.");
                Console.Write("Enter the \"ID\" for which you want to search: ");
                Gray();
                string idString = Console.ReadLine();

                if (idString.ToUpper() != "E" && idString.ToUpper() != "EXIT")
                {
                    int id;

                    if (int.TryParse(idString, out id))
                    {
                        try
                        {
                            DisplayPersonById(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nError during \"ListAllPerson\"...");
                            Console.WriteLine(ex);
                            PressKeyToContinue();
                            throw;
                        }
                    }
                    else
                    {
                        DarkYellow();
                        Console.Clear();
                        Console.WriteLine("\n\nIncorrect input....");
                        Console.WriteLine($"You entered: \"{idString}\"");
                        Gray();
                        Console.WriteLine("\"ESC\" or \"Backspace\" to switch back to the \"Main-Menu\" ,otherwise press any Key to continue...");
                        ConsoleKeyInfo input = Console.ReadKey();
                        if (input.Key == ConsoleKey.Escape || input.Key == ConsoleKey.Backspace)
                            exit = true;

                        Console.Clear();
                    }
                }
                else
                    exit = true;
            } while (!exit);
            Console.Clear();
        }

        /// <summary>
        /// Gets Person from Database and displays them on the Console Window
        /// </summary>
        /// <param name="id">id from Person</param>
        public static void DisplayPersonById(int id)
        {
            Person person = new Person();
            person = Database.GetPersonById(id);

            if (person != null)
            {
                Console.WriteLine("\nID \t | Firstname \t | Lastname");
                Console.WriteLine("_________________________________________");

                Console.WriteLine($"{person.Id} \t | {person.FirstName} \t | {person.LastName}");


                PressKeyToContinue();
            }
            else
            {
                Yellow();
                Console.WriteLine($"Cannot find persons/persons with the ID: \"{id}\"");
                PressKeyToContinue();
            }
        }

        #endregion

        /// <summary>
        /// Creates a new Person by adding the firstname and the lastname
        /// </summary>
        private static void CreateNewPerson()
        {
            ConsoleKeyInfo keyInfo;

            do
            {
                Red();
                Console.WriteLine("\n\n\t# # # C R E A T E   N E W   P E R S O N - M E N U # # #");
                Console.WriteLine("To leave the menu write (e)xit into the console window and press the enter key.");
                Console.Write("Enter the \"Firsname\" of your new Person: ");
                Gray();
                string firstName = Console.ReadLine();
            
                Red();
                Console.Write("Enter the \"Lastname\" of your new Peerson: ");
                Gray();
                string lastName = Console.ReadLine();

                if (firstName.ToUpper() != "E" && firstName.ToUpper() != "EXIT" && lastName.ToUpper() != "E" && lastName.ToUpper() != "EXIT")
                {
                    Red();
                    Console.WriteLine("\nDo you really want to create this Person? (y/n)");
                    Console.WriteLine("Choose by pressing the key of the option. If you want to switch back to main-menu press the \"ESC\" key, or the \"Backspace\". ");
                    Gray();
                    keyInfo = Console.ReadKey();

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Y:
                            Person person = Database.CreateNewPerson(firstName, lastName);

                            if (person != null)
                            {
                                Cyan();
                                Console.WriteLine("\nNew Person created: \n");
                                Gray();
                                Console.WriteLine("\nID \t | Firstname \t | Lastname");
                                Console.WriteLine("_________________________________________");

                                Console.WriteLine($"{person.Id} \t | {person.FirstName} \t | {person.LastName}");

                                PressKeyToContinue();
                            }
                            else
                            {
                                Yellow();
                                Console.WriteLine($"\nCannot find newely created Person.");
                                PressKeyToContinue();
                            }
                            break;
                        case ConsoleKey.N:
                            Console.WriteLine("\n\nYou don't want to create this Person:");
                            Console.WriteLine($"Firstname: \"{firstName}\"");
                            Console.WriteLine($"Lastname: \"{lastName}\"");
                            PressKeyToContinue();
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            break;
                        case ConsoleKey.Backspace:
                            Console.Clear();
                            break;
                        default:
                            Yellow();
                            Console.WriteLine($"You pressed the wrong key, your input was: \"{keyInfo.Key}\"");
                            PressKeyToContinue();
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    // '\u0066' -> Unicode for the Escape key
                    keyInfo = new ConsoleKeyInfo('\u0066', ConsoleKey.Escape, false, false, false);
                }
            } while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Backspace);

        }

        #endregion

        #region Modify existing Person

        private static void ModifyExistingPerson()
        {
            ConsoleKeyInfo keyInfo;
            string idString;

            do
            {
                // List all existing Person
                Green();
                Console.WriteLine("\n\n\t# # # M O D I F Y   E X I S T I N G   P E R S O N - M E N U # # #");
                Console.WriteLine("\nList of all existing Persons:\n");
                Gray();
                ListAllPersons();

                // Let the user choose Person by ID
                Green();
                Console.WriteLine("\n\nTo leave the menu write (e)xit into the console window and press the enter key.");
                Console.Write("Please Enter the \"ID\" of the Person which you want to modify: ");
                idString = Console.ReadLine();
                int id;

                if (idString.ToUpper() != "E" && idString.ToUpper() != "EXIT")
                { 

                    if (int.TryParse(idString, out id))
                    {
                        Person p = Database.GetPersonById(id);
                        // Verify if entry with the id which user has entered exist in the database;
                        if (p != null)
                        {
                            do
                            {
                                Console.Clear();
                                Green();
                                Console.WriteLine("\n\n\t# # # M O D I F Y   E X I S T I N G   P E R S O N - M E N U # # #");
                                Console.WriteLine("Your choice:");
                                Console.WriteLine($"Id: \"{p.Id}\"");
                                Console.WriteLine($"Firstname: \"{p.FirstName}\"");
                                Console.WriteLine($"Lastname: \"{p.LastName}\"");
                                Console.WriteLine("\n\nChoose by pressing the number key of the option.");
                                Console.WriteLine("1) Modify Firstname of the Person.");
                                Console.WriteLine("2) Modify Lastname of the Person.");
                                Console.WriteLine("3) Modify Firstname and Lastname of the Person");
                                Gray();
                                Console.WriteLine("\"ESC\" or \"Backspace\" to choose a other Person to modify.");
                                keyInfo = Console.ReadKey();

                                switch (keyInfo.Key)
                                {
                                    case ConsoleKey.NumPad1:
                                        ModifyFirstname(id);
                                        p = Database.GetPersonById(id);
                                        break;
                                    case ConsoleKey.D1:
                                        p = ModifyFirstname(id);
                                        break;
                                    case ConsoleKey.NumPad2:
                                        p = ModifyLastName(id);
                                        break;
                                    case ConsoleKey.D2:
                                        p = ModifyLastName(id);
                                        break;
                                    case ConsoleKey.NumPad3:
                                        p = ModifyPerson(id);
                                        break;
                                    case ConsoleKey.D3:
                                        p = ModifyPerson(id);
                                        break;
                                    case ConsoleKey.Escape:
                                        Console.Clear();
                                        // Back to top
                                        break;
                                    case ConsoleKey.Backspace:
                                        Console.Clear();
                                        // Back to top
                                        break;
                                    default:
                                        Yellow();
                                        Console.WriteLine($"You pressed the wrong key, your input was: \"{keyInfo.Key}\"");
                                        PressKeyToContinue();
                                        Console.Clear();
                                        break;
                                }   
                            } while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Backspace);
                        }
                        else
                        {
                            Yellow();
                            Console.WriteLine($"\nPerson with \"{id}\" does not exist in the database!");
                            PressKeyToContinue();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Yellow();
                        Console.WriteLine($"\nYou entered a not valid integer number, your input was: \"{idString}\"");
                        PressKeyToContinue();
                        Console.Clear();
                    }
                }
            } while (idString.ToUpper() != "E" && idString.ToUpper() != "EXIT");
            Console.Clear();
        }

        /// <summary>
        /// Modifies Firstname and Lastname of Person and displays it afterwards on the console window.
        /// </summary>
        private static Person ModifyPerson(int id)
        {
            // Ask User for new Firstname and new Lastname of the Person
            Console.Clear();
            Green();
            Console.Write("\n\nPlease enter the new \"Firstname\" of the Person: ");
            Gray();
            string firstName = Console.ReadLine();
            Green();
            Console.Write("\n\nPlease enter the new \"Lastname\" of the Person: ");
            Gray();
            string lastName = Console.ReadLine();

            // Modify existing Person
            Green();

            if (Database.ModifyPerson(id, firstName, lastName, Database.Modify.All))
                Console.WriteLine("Person successfully modified.\n");
            else
                Console.WriteLine("Person NOT successfully modified!");

            Gray();
            // Display modified Person
            DisplayPersonById(id);
            return Database.GetPersonById(id);
        }

        /// <summary>
        /// Modify the firstname of a Person.
        /// </summary>
        /// <param name="id">DB-ID of the Person</param>
        private static Person ModifyFirstname(int id)
        {
            // Ask User for new Firstname and new Lastname of the Person
            Console.Clear();
            Green();
            Console.Write("\n\nPlease enter the new \"Firstname\" of the Person: ");
            Gray();
            string firstName = Console.ReadLine();
            
            // Modify existing Person
            Green();
            if (Database.ModifyPerson(id, firstName, null, Database.Modify.Firstname))
                Console.WriteLine("Person successfully modified.");
            else
                Console.WriteLine("Person NOT successfully modified!");

            Gray();
            // Display modified Person
            DisplayPersonById(id);
            return Database.GetPersonById(id);
        }

        /// <summary>
        /// Modify the Lastname of a Person.
        /// </summary>
        /// <param name="id">DB-ID of the Person</param>
        private static Person ModifyLastName(int id)
        {
            // Ask User for new Firstname and new Lastname of the Person
            Console.Clear();
            Green();
            Console.Write("\n\nPlease enter the new \"Lastname\" of the Person: ");
            Gray();
            string lastName = Console.ReadLine();

            // Modify existing Person
            Green();
            if (Database.ModifyPerson(id, null, lastName, Database.Modify.Lastname))
                Console.WriteLine("Person successfully modified.");
            else
                Console.WriteLine("Person NOT successfully modified!");

            Gray();
            // Display modified Person
            DisplayPersonById(id);
            return Database.GetPersonById(id);
        }

        #endregion
    }
}

