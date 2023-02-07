using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PersonManagement.Models
{
    public class DBProvisioning
    {

        /// <summary>
        /// Creates Database
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="dbServerName">DB Servername</param>
        /// <returns>Database created successfully => TRUE/Database not created, or Error</returns>
        public static bool CreateDatabaseIfNotExists(out string error, string dbServerName)
        {
            error = null;
            string er = null;

            string commandString = $"IF DB_ID('dbPerson') IS NULL \r\n" +
                                   $"BEGIN \r\n" +
                                   $"    CREATE DATABASE dbPerson \r\n" +
                                   $"END \r\n" +
                                   $";";

            try
            {
                string connectionString = SQLHelper.CreateConnectionStringFromObject(out er, dbServerName, "master", null, null);
                error += er;

                if (null == connectionString)
                {
                    error += $"Error during creation of DB-ConnectionString:\r\n\r\n{error}";
                    return false;
                }

                if (SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
                {
                    error += er;
                    return true;
                }

                error += er;

                if (string.IsNullOrEmpty(error))
                {
                    error = $"Error during Creation of Database dbPerson!\r\n{error}\r\n\r\n";
                }
            }
            catch (Exception ex)
            {
                error += $"Error during creation of Database dbPerson:\r\n{ex.Message}\r\n\r\n";
                throw;
            }

            return false;
        }

        /// <summary>
        /// Creates necessary Tables
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="connectionString">DB-Connectionstring</param>
        /// <returns>Tables are created => TRUE/Tables are not created => FALSE</returns>
        public static bool CreateTables(out string error, string connectionString)
        {
            error = null;
            string er = null;

            string commandString = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblPerson' and xtype='U')\r\n" +
                                   "BEGIN\r\n" +
                                   "    CREATE TABLE tblPerson\r\n" +
                                   "    (\r\n" +
                                   "        Id INT IDENTITY PRIMARY KEY NOT NULL,\r\n" +
                                   "        FirstName VARCHAR(150) NOT NULL,\r\n" +
                                   "        LastName VARCHAR(150) NOT NULL,\r\n" +
                                   "        Age INT DEFAULT(0)\r\n" +
                                   "    )\r\n" +
                                   "END\r\n" +
                                   ";";

            //System.Diagnostics.Debug.WriteLine($"\n-----------------------");
            //System.Diagnostics.Debug.WriteLine(commandString);
            //System.Diagnostics.Debug.WriteLine($"-----------------------\n");
            //System.Diagnostics.Debugger.Break();

            if (SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
            {
                error += er;
                return true;
            }

            error += er;

            if (string.IsNullOrEmpty(error))
            {
                error = $"Error during Creation of Person!\r\n{error}\r\n\r\n";
            }

            return false;
        }

        /// <summary>
        /// Creates all necessary Stored Procedures
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="connectionString">DB-Connectionstring</param>
        /// <returns>SP's created successfully => TRUE/Error during creation => FALSE</returns>
        public static bool CreateStoredProcedures(out string error, string connectionString)
        {
            error = null;
            string er = null;

            string commandString = $"CREATE OR ALTER PROCEDURE spInsertPersonIfNotExists @firstName VARCHAR(150), @lastName VARCHAR(150), @age INT \r\n" +
                                   $"AS \r\n" +
                                   $"BEGIN \r\n" +
                                   $"   INSERT INTO tblPerson(FirstName, LastName, Age) \r\n" +
                                   $"    SELECT @firstName, \r\n" +
                                   $"           @lastName, \r\n" +
                                   $"           @age \r\n" +
                                   $"    WHERE NOT EXISTS \r\n" +
                                   $"        (SELECT 1 \r\n" +
                                   $"         FROM tblPerson WITH(NOLOCK) \r\n" +
                                   $"         WHERE 1 = 1 \r\n" +
                                   $"             AND FirstName = @firstName \r\n" +
                                   $"             AND LastName = @lastName \r\n" +
                                   $"             AND Age = @age) \r\n" +
                                   $"END " +
                                   $";";

            if (!SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
            {
                error += er;

                if (string.IsNullOrEmpty(error))
                {
                    error = $"Error during Creation of spInsertPersonIfNotExists!\r\n{error}\r\n\r\n";
                }

                return false;
            }

            error += er;

            commandString = $"CREATE OR ALTER PROCEDURE spInsertPerson @firstName VARCHAR(150), \r\n" +
                            $"                                         @lastName VARCHAR(150), \r\n" +
                            $"                                         @age INT \r\n" +
                            $"AS \r\n" +
                            $"BEGIN \r\n" +
                            $"   INSERT INTO tblPerson(FirstName, LastName, Age) \r\n" +
                            $"   VALUES \r\n" +
                            $"   (@firstName, @lastName, @age) \r\n" +
                            $"END \r\n" +
                            $";";

            if (!SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
            {
                error += er;

                if (string.IsNullOrEmpty(error))
                {
                    error = $"Error during Creation of spInsertPerson!\r\n{error}\r\n\r\n";
                }

                return false;
            }

            error += er;

            commandString = $"CREATE OR ALTER PROCEDURE spUpdatePerson @id INT, @firstName VARCHAR(150), @lastName VARCHAR(150), @age INT \r\n" +
                            $"AS \r\n" +
                            $"BEGIN \r\n" +
                            $"    UPDATE tblPerson \r\n" +
                            $"    SET FirstName = @firstName, \r\n" +
                            $"        LastName = @lastName, \r\n" +
                            $"        Age = @age \r\n" +
                            $"    WHERE Id = @id \r\n" +
                            $"END \r\n" +
                            $";";

            if (!SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
            {
                error += er;

                if (string.IsNullOrEmpty(error))
                {
                    error += $"Error during Creation of spUpdatePerson!\r\n{error}\r\n\r\n";
                }

                return false;
            }

            error += er;

            commandString = $"CREATE OR ALTER PROCEDURE spDeletePerson @id INT \r\n" +
                            $"AS \r\n" +
                            $"BEGIN \r\n" +
                            $"    DELETE FROM tblPerson \r\n" +
                            $"    WHERE Id = @id \r\n" +
                            $"END \r\n" +
                            $";";

            if (!SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
            {
                error += er;

                if (string.IsNullOrEmpty(error))
                {
                    error += $"Error during Creation of spUpdatePerson!\r\n{error}\r\n\r\n";
                }

                return false;
            }

            error += er;

            return true;
        }

        /// <summary>
        /// Insert Test-Data
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="connectionString">DB-Connectionstring</param>
        /// <returns>Data successfullly inserted => TRUE/Data not successfully inserted, or Error => FALSE</returns>
        public static bool InsertDataIfNotExists(out string error, string connectionString)
        {
            error = null;
            string er = null;

            string commandString = $"EXEC spInsertPersonIfNotExists 'Jonny', 'Johnson', 20 \r\n" +
                                    "; \r\n" +
                                    "EXEC spInsertPersonIfNotExists 'Melanie', 'Winter', 33 \r\n" +
                                    "; \r\n" +
                                    "EXEC spInsertPersonIfNotExists 'Walter', 'Mayer', 18 \r\n" +
                                    "; \r\n" +
                                    "EXEC spInsertPersonIfNotExists 'Michaela', 'Mayer', 45 \r\n" +
                                    ";";

            if (SQLHelper.ExecuteCommand(out er, ref connectionString, commandString))
            {
                error += er;
                return true;
            }

            error += er;

            if (string.IsNullOrEmpty(error))
            {
                error = $"Error during Creation of Person!\r\n{error}\r\n\r\n";
            }

            return false;
        }
    }
}
