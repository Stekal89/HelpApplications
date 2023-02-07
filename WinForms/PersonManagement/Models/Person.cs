using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonManagement.Models
{
    public class Person
    {
        #region Properties

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; } = 0;

        #endregion

        #region Functions

        private bool ValidateInput(out string error, string firstName, string lastName, int age)
        {
            error = null;
            bool success = true;

            if (string.IsNullOrEmpty(firstName))
            {
                error += $"Firstname cannot be empty!";
                success = false;
            }

            if (string.IsNullOrEmpty(lastName))
            {
                error += $"LastName cannot be empty!";
                success = false;
            }
            
            if (age <= 0)
            {
                error += $"Age must be greater than 0!";
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Create a new Person
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="connectionString">DB-Connection string</param>
        /// <param name="firstName">Firstname of the new Person</param>
        /// <param name="lastName">Lastname of the new Person</param>
        /// <param name="age">Age of the new Person</param>
        /// <returns>Person successfully created => TRUE/Person not successfully created => FALSE</returns>
        public bool CreateNewPerson(out string error, string connectionString, string firstName, string lastName, int age)
        {
            error = null;
            string er = null;

            if (!ValidateInput(out er, firstName, lastName, age))
            {
                error += er;
                return false;
            }

            try
            {
                string commandString = $"EXEC spInsertPerson '{firstName}', '{lastName}', {age} \r\n" +
                                    $";";

                if (!SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
                {
                    error += er;
                    error += $"Error during creation of Person!\r\n{error}\r\n\r\n";
                    return false;
                }
                error += er;

                // Initialize values of this Object
                FirstName = firstName;
                LastName = lastName;
                Age = age;

                using (SqlConnection sqlConnection = SQLHelper.CreateConnection(out er, connectionString))
                {
                    error += er;

                    if (null == sqlConnection || !string.IsNullOrEmpty(error))
                    {
                        error += $"Error during creation of Database connection!\r\n{error}\r\n\r\n";
                        return false;
                    }

                    // Get ID from the current created Person
                    string query = $"SELECT IDENT_CURRENT('tblPerson') AS Id\r\n" +
                                   $";";

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        if (sqlConnection.State != ConnectionState.Closed)
                        {
                            sqlConnection.Close();
                        }
                        sqlConnection.Open();
                    }

                    SqlCommand command = new SqlCommand(query, sqlConnection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Read Data from the Database
                        while (reader.Read())
                        {
                            if (int.TryParse(reader["Id"].ToString(), out int id))
                            {
                                Id = id;
                            }
                            else
                            {
                                error += $"Error during creation of Person!\r\nCannot parse ID of created Person.\r\n\r\n";
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error += $"Error during Creation of Person!\r\n{ex.Message}";
            }

            if (!string.IsNullOrEmpty(error))
            {
                error += $"Error during Creation of Person!\r\n{error}\r\n\r\n";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Modify actual person
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="connectionString">DB-Connection string</param>
        /// <param name="newFirstName">new Firstname of the person</param>
        /// <param name="newLastName">new Lastname of the person</param>
        /// <param name="newAge">new Age of the person</param>
        /// <returns>Person successfully modified => TRUE/Person not modified, or Error => FALSE</returns>
        public bool ModifyPerson(out string error, string connectionString, string newFirstName, string newLastName, int newAge)
        {
            error = null;
            string er = null;

            if (!ValidateInput(out er, newFirstName, newLastName, newAge))
            {
                error += er;
                return false;
            }

            try
            {
                string commandString = $"EXEC spUpdatePerson {Id}, '{newFirstName.Trim()}', '{newLastName.Trim()}', {newAge} \r\n" +
                                        ";";

                if (!SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
                {
                    error += er;
                    error += $"Error during modification of Person!\r\n{error}\r\n\r\n";
                    return false;
                }

                error += er;
            }
            catch (Exception ex)
            {
                error += $"Error during modification of Person!\r\n{ex.Message}";
            }
            
            if (!string.IsNullOrEmpty(error))
            {
                error += $"Error during modification of Person!\r\n{error}\r\n\r\n";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Delete Person
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="connectionString">DB-Connectionstring</param>
        /// <returns>Person successfully deleted => TRUE/Person not successfully deleted, or Error => FALSE</returns>
        public bool DeletePerson(out string error, string connectionString)
        {
            error = null;
            string er = null;

            if (Id <= 0)
            {
                error += $"Invalid Id of Person!\r\n" +
                         $"Id = \"{Id}\"\r\n\r\n";
                return false;
            }

            try
            {
                string commandString = $"EXEC spDeletePerson {Id} \r\n" +
                                        ";";

                if (!SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
                {
                    error += er;
                    error += $"Error during deleting of Person!\r\n{error}\r\n\r\n";
                    return false;
                }

                error += er;
            }
            catch (Exception ex)
            {
                error += $"Error during deleting of Person!\r\n{ex.Message}";
            }

            if (!string.IsNullOrEmpty(error))
            {
                error += $"Error during deleting of Person!\r\n{error}\r\n\r\n";
                return false;
            }

            return true;
        }

        #endregion
    }
}
