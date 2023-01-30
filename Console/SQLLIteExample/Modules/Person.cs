using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLLIteExample.Modules
{
    public class Person
    {
        #region Properties

        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int Age { get; set; } = 0;

        #endregion

        #region Functions

        public bool CreateTableIfNotExist(SqliteConnection conn)
        {
            SqliteCommand command;

            string commandString = "CREATE TABLE IF NOT EXISTS tblPerson\r\n" +
                                  "(\r\n" +
                                  "    Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, \r\n" +
                                  "    FirstName VARCHAR(150) NOT NULL,\r\n" +
                                  "    LastName VARCHAR(159) NOT NULL\r\n," +
                                  "    AGE INT DEFAULT(0)\r\n" +
                                  ")";
            try
            {
                command = conn.CreateCommand();
                command.CommandText = commandString;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Program.WriteError($"Person.CreateTableIfNotExist-Error:\n{ex.Message}\n");
            }
         
            return false;
        }

        /// <summary>
        /// Creates Person
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="dbConn">Database-Connection object (SqliteConnection)</param>
        /// <returns>Insert successfully done => TRUE/Insert not successfully, or Error => FALSE</returns>
        public bool InsertPerson(out string? error, SqliteConnection dbConn)
        {
            error = null;
            string? er = null;

            bool bContinue = true;
            string? firstName = (!string.IsNullOrEmpty(FirstName) ? FirstName.Trim() : null);
            string? lastName = (!string.IsNullOrEmpty(LastName) ? LastName.Trim() : null);

            if (string.IsNullOrEmpty(firstName))
            {
                er += "FirstName is NULL!\r\n";
                bContinue = false;
            }
            if (string.IsNullOrEmpty(lastName))
            {
                er += "LastName is NULL!\r\n";
                bContinue = false;
            }

            if (!bContinue)
            {
                error += $"Cannot create Person!\r\n" +
                         $"{er}\r\n\r\n";
                return false;
            }

            er = null;

            string commandString = $"INSERT INTO tblPerson (FirstName, LastName, Age)\r\n" +
                                   $"VALUES\r\n" +
                                   $"(" +
                                   $"    '{firstName}',\r\n" +
                                   $"    '{lastName}',\r\n" +
                                   $"    {Age}\r\n" +
                                   $")";

            if (SQLiteHelper.ExecuteCommand(out er, dbConn, commandString))
            {
                error += er;
                return true;
            }

            error += er;

            if (string.IsNullOrEmpty(error))
            {
                error += $"Error during Creation of Person!\r\n\r\n";
            }

            return false;
        }

        #endregion
    }
}
