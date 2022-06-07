using Connection_XAMPP_MySQL.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Required to use the connectionstring using the app.config
using System.Configuration;

namespace Connection_XAMPP_MySQL
{
    public class Database
    {
        //private static string conString = "server = localhost; user id = root; database = dbtest";
        // Tutorial: https://www.youtube.com/watch?v=MQ8t2bHtGlg
        // Section from app.config: "Connection_XAMPP_MySQL.Properties.Settings.Setting"
        private static string conString = ConfigurationManager.ConnectionStrings["Connection_XAMPP_MySQL.Properties.Settings.Setting"].ConnectionString;
          
        public enum Modify
        {
           Firstname = 0,
           Lastname = 1,
           All = 2

        }

        #region Methods

        #region Main-Methods
        
        /// <summary>
        /// Method to execute a query, which returns a person -> Person
        /// </summary>
        /// <param name="query">SQL-Query</param>
        /// <returns>Person</returns>
        private static Person QueryForPerson(string query)
        {
            Person person = new Person();
            
            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.CommandTimeout = 60;
                        connection.Open();

                        using (MySqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                int identity;
                                while (dataReader.Read())
                                {
                                    int.TryParse(dataReader.GetString("Id"), out identity);
                                    person = new Person()
                                    {
                                        Id = identity,
                                        FirstName = dataReader.GetString("Firstname"),
                                        LastName = dataReader.GetString("Lastname")
                                    };
                                }
                                return person;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nERROR during execution fo query for \"Person\":");
                    Console.WriteLine(query);
                    Console.WriteLine(ex);
                }
            }
            return null;
        }

        /// <summary>
        /// Method to execute a query, which returns a list of persons -> List<Person>
        /// </summary>
        /// <param name="query">SQL-Query</param>
        /// <returns>List of Persons</returns>
        private static List<Person> QueryForPersonList(string query)
        {
            List<Person> persons = new List<Person>();
            
            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.CommandTimeout = 60;
                        connection.Open();

                        using (MySqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                int id;

                                while (dataReader.Read())
                                {
                                    int.TryParse(dataReader.GetString("Id"), out id);
                                    persons.Add(new Person()
                                    {
                                        Id = id,
                                        FirstName = dataReader.GetString("Firstname"),
                                        LastName = dataReader.GetString("Lastname")
                                    });
                                }
                                return persons;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nERROR during execution fo query for \"PersonList\":");
                    Console.WriteLine(query);
                    Console.WriteLine(ex);
                }
            }
            return null;
        }

        /// <summary>
        /// Method to execute a query, which returns an integer
        /// </summary>
        /// <param name="query">SQL-Query</param>
        /// <returns>Integer</returns>
        private static int GetNumbIntegerResult(string query)
        {
            int result = -1;

            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.CommandTimeout = 60;
                        connection.Open();

                        using (MySqlDataReader dataReader = command.ExecuteReader())
                        { 

                            if (dataReader.HasRows)
                            {
                                int tableResult;
                                while (dataReader.Read())
                                {
                                    int.TryParse(dataReader.GetString("Count"), out tableResult);
                                    result = tableResult;
                                }
                                return result;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR during execution of Query....");
                    Console.WriteLine(ex);
                }
            }
            return result;
        }

        #endregion

        #region Get all Persons
        
        /// <summary>
        /// Selects all Persons and returns it as a List of Persons
        /// </summary>
        /// <returns>All Persons which are existing in the database</returns>
        public static List<Person> GetAllPersons()
        {
            string query = @"
                                SELECT * 
                                FROM tblperson;
                               ";
            return QueryForPersonList(query);
        }

        #endregion

        #region Get all Persons by Name

        /// <summary>
        /// Search for all persons with Firstname \"firstName\" and returns the results in a list of Persons.
        /// </summary>
        /// <param name="firstName">Firstname of persons, which you want to find</param>
        /// <returns>List of Person</returns>
        public static List<Person> GetAllPersonsByFirstName(string firstName)
        {
            string query = $@"
                                SELECT * 
                                FROM tblperson
                                where Firstname = '{firstName}';
                               ";
            return QueryForPersonList(query);
        }

        /// <summary>
        /// Search for all persons with Lastname \"lastName\" and returns the results in a list of Persons.
        /// </summary>
        /// <param name="firstName">Firstname of persons, which you want to find</param>
        /// <returns>List of Person</returns>
        public static List<Person> GetAlPersonsByLastName(string lastName)
        {
            string query = $@"
                                SELECT * 
                                FROM tblperson
                                where Lastname = '{lastName}';
                               ";
            return QueryForPersonList(query);
        }

        /// <summary>
        /// Search for all persons with Firstname \"firstName\" and Lastname \"lastName\" and returns the results in a list of Persons.
        /// </summary>
        /// <param name="firstName">Firstname of persons, which you want to find</param>
        /// /// <param name="lastName">Lastname of persons, which you want to find</param>
        /// <returns>List of Person</returns>
        public static List<Person> GetAllPersonsByFirstAndLastName(string firstName, string lastName)
        {
            string query = $@"
                                SELECT * 
                                FROM tblperson
                                where Firstname = '{firstName}' and Lastname = '{lastName}';
                               ";
            return QueryForPersonList(query);
        }

        #endregion

        #region Get Peron by ID

        /// <summary>
        /// Search for a Person by Id.
        /// </summary>
        /// <param name="id">Id of the Person, which you want to find.</param>
        /// <returns>List of Person/Peoples</returns>
        public static Person GetPersonById(int id)
        {
            string query = $@"
                                SELECT * 
                                FROM tblperson
                                where Id = '{id}';
                               ";
            return QueryForPerson(query);   
        }

        #endregion

        #region Create a new Person

        /// <summary>
        /// Create a new Person
        /// </summary>
        /// <param name="firstName">Firstname of the new Person</param>
        /// <param name="lastName">Lastname of the new Person</param>
        /// <returns>Success: new created Person/Failure: NULL</returns>
        public static Person CreateNewPerson(string firstName, string lastName)
        {
            // Insert

            Person person = new Person();

            // Before executing the insert statement make a slect COUNT(ID) to see how much rows exist -> save the result into a variable
            int beforeRowCount = GetTableRowCount();
            Debug.Indent();
            Debug.WriteLine($"Count of rows before insert data: \"{beforeRowCount}\"");
            Debug.Unindent();

            string insert = $@"
                                INSERT INTO tblPerson (Firstname, Lastname) VALUES
                                ('{firstName}','{lastName}')
                               ";

            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    { 
                        command.CommandTimeout = 60;
                        connection.Open();

                        // Execute the insert -> Add the new User
                        using (MySqlDataReader dataReader = command.ExecuteReader())
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR during execution of Query....");
                    Console.WriteLine(ex);
                }
            }

            // Execute again the select COUNT(*) and verify if there is a new row
            int afterRowCount = GetTableRowCount();
            Debug.Indent();
            Debug.WriteLine($"Count of rows after insert data: \"{afterRowCount}\"");
            Debug.Unindent();

            if (beforeRowCount < afterRowCount)
            {
                // load the new entry and return it as Person!
                return LoadLastRow();
            }
            return null;
        }

        /// <summary>
        /// Gets the Row-Count of the table tblPerson and returns it.
        /// </summary>
        /// <returns>Table-RowCount</returns>
        private static int GetTableRowCount()
        {
            string query = $@"
                                SELECT COUNT(*) AS Count
                                FROM tblperson;
                               ";
            return GetNumbIntegerResult(query);
        }

        /// <summary>
        /// Loads the last Table-Entry and returns it as Person
        /// </summary>
        /// <returns>Last Person in table</returns>
        private static Person LoadLastRow()
        {
            string query = $@"
                                SELECT *
                                FROM tblperson
                                ORDER BY ID DESC
                                LIMIT 1;
                               ";
            return QueryForPerson(query);
        }

        #endregion

        public static bool ModifyPerson(int id, string newFirstName, string newLastName, Modify modify)
        {
            string update = null;

            if (modify == Modify.All)
            {
                if (newFirstName != null && newLastName != null)
                {
                    update = $@"
                            UPDATE tblPerson 
                            SET Firstname = '{newFirstName}', Lastname = '{newLastName}'
                            WHERE Id = {id};
                            ";
                }
            }
            else if (modify == Modify.Firstname)
            {
                if (newFirstName != null)
                {
                    update = $@"
                                UPDATE tblPerson 
                                SET Firstname = '{newFirstName}'
                                WHERE Id = {id};
                                ";
                }
            }
            else if (modify == Modify.Lastname)
            {
                if (newLastName != null)
                {
                    update = $@"
                                UPDATE tblPerson 
                                SET Lastname = '{newLastName}'
                                WHERE Id = {id};
                                ";
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("One, or some Parameters are wrong in ModifyPerson()");
                Console.WriteLine($"Id: \"{id}\"");
                Console.WriteLine($"New Frstname: \"{newFirstName}\"");
                Console.WriteLine($"New Lastname: \"{newLastName}\"");
                Console.WriteLine($"Modify-Type: \"{modify}\"");
                return false;
            }
            
            if (update != null || update != "")
            {
                // Modify Person
                using (MySqlConnection connection = new MySqlConnection(conString))
                {
                    try
                    {
                        using (MySqlCommand command = new MySqlCommand(update, connection))
                        {
                            command.CommandTimeout = 60;

                            connection.Open();
                            // Execute the insert -> Add the new User
                            using (MySqlDataReader dataReader = command.ExecuteReader())
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR during execution of Query....");
                        Console.WriteLine(ex);
                    }
                }

                // Select and return new created Person
                Person person = GetPersonById(id);

                if (modify == Modify.All)
                {
                    if (person.FirstName == newFirstName && person.LastName == newLastName)
                        return true;
                }
                else if (modify == Modify.Firstname)
                {
                    if (person.FirstName == newFirstName)
                        return true;
                }
                else if (modify == Modify.Lastname)
                {
                    if (person.LastName == newLastName)
                        return true;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("New-Firstname and New-Lastname are NULL! One of them musst be setted!");
                Console.WriteLine($"Firstname: \"{newFirstName}\"");
                Console.WriteLine($"Lastname: \"{newLastName}\"");
                Console.WriteLine($"Modify-Type: \"{modify}\"");
                Program.PressKeyToContinue();
                return false;
            }
            return false;
        }

        #endregion
    }
}
