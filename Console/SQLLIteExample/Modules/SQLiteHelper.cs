using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLLIteExample.Modules
{
    public static class SQLiteHelper
    {
        public static string? DBPath { get; set; }

        /// <summary>
        /// Create DB-File if not exists
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="dbPath">DB-File</param>
        /// <returns>File exists/Error</returns>
        public static bool CreateDataBaseIfNotExists(out string? error, string? dbPath)
        {
            error = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(dbPath))
                {
                    if (!System.IO.File.Exists(dbPath))
                    {
                        Console.WriteLine($"DB-File '{dbPath}' not exists, creating...");

                        // Verify if folder exists
                        string folderDirectory = System.IO.Path.GetDirectoryName(dbPath);
                        if (!System.IO.Directory.Exists(folderDirectory))
                        {
                            System.IO.Directory.CreateDirectory(folderDirectory);
                        }

                        // Create DB
                        using (var file = System.IO.File.Create(dbPath))
                        {

                        }
                    }
                }
                else
                {
                    error = "DB-Path is NULL!\r\n";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = $"Error during creation of DB-File!\r\n{ex.Message}\r\n";
                return false;
            }
           
            return true;
        }

        /// <summary>
        /// Create db-connection to the sqlite db
        /// </summary>
        /// <param name="dbPath">Path of the DB-File</param>
        /// <returns>SQL-Connection/NULL</returns>
        public static SqliteConnection? CreateConnection(out string? error, string dbPath)
        {
            error = null;

            if (string.IsNullOrEmpty(dbPath))
            {
                dbPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\tmpDB.sqlite";
            }

            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                DataSource = dbPath
            };
            string connectionString = connectionStringBuilder.ToString();

            SqliteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SqliteConnection(connectionString);
            // Open the connection:
            try
            {
                sqlite_conn.Open();
                return sqlite_conn;
            }
            catch (Exception ex)
            {
                error += $"Error during creation of DB-Connection!\r\n{ex.Message}\r\n";
            }
            return null;
        }

        /// <summary>
        /// Executes SQLite-Command like INSERT, UPDATE, ...
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="dbConn">dbConnection</param>
        /// <param name="commandString">Command as text</param>
        /// <returns>Command successfully executed => TRUE/Command not executed, or Error => FALSE</returns>
        public static bool ExecuteCommand(out string? error, SqliteConnection? dbConn, string commandString)
        {
            error = null;
            string? er = null;

            if (null == dbConn)
            {
                dbConn = CreateConnection(out er, DBPath);
                error += er;
                
                if (null == dbConn)
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        error += $"Cannot create DB-Connection!\r\n";
                    }

                    return false;
                }
            }

            if (string.IsNullOrEmpty(commandString))
            {
                error += "SQLiteHelper.ExecuteCommand-Error:\r\nCommand-String is NULL or EMPTY!\r\n";
                return false;
            }

            SqliteCommand command = new SqliteCommand();

            try
            {
                command = dbConn.CreateCommand();
                command.CommandText = commandString;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error += $"SQLiteHelper.ExecuteCommand-Error:\r\n" +
                                   $"------------------\r\n"       +
                                   $"COMMAND:\r\n"                 +
                                   $"{commandString}\r\n"          +
                                   $"------------------\r\n\r\n"   +
                                   $"### ERROR ###: {ex.Message}\n";
            }

            return false;
        }
    }
}
