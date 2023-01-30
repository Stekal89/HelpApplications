using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLLIteExample.Modules
{
    public class DBProvisioning
    {
        /// <summary>
        /// Creates necessary Tables
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="dbConn">DB-Connection Object</param>
        /// <returns>Tables are created => TRUE/Tables are not created => FALSE</returns>
        public static bool CreateTables(out string? error, SqliteConnection? dbConn)
        {
            error = null;
            string? er = null;

            string commandString = "CREATE TABLE IF NOT EXISTS tblPerson\r\n" +
                                  "(\r\n" +
                                  "    Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, \r\n" +
                                  "    FirstName VARCHAR(150) NOT NULL,\r\n" +
                                  "    LastName VARCHAR(159) NOT NULL\r\n," +
                                  "    AGE INT DEFAULT(0)\r\n" +
                                  ")";

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
    }
}
