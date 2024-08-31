using Microsoft.Data.Sqlite;
using SQLLIteExample.Modules;

namespace SQLLIteExample
{
    internal class Program
    {
        #region Help-Functions

        /// <summary>
        /// Write Success-Message to Console
        /// </summary>
        /// <param name="msg">Message</param>
        public static void WriteSuccess(string msg)
        {
            ConsoleColor actColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(msg);
            Console.ForegroundColor = actColor;
        }

        /// <summary>
        /// Write Success-Warning to Console
        /// </summary>
        /// <param name="msg">Message</param>
        public static void WriteWarning(string msg)
        {
            ConsoleColor actColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n{msg}");
            Console.ForegroundColor = actColor;
        }

        /// <summary>
        /// Write Success-Error to Console
        /// </summary>
        /// <param name="msg">Message</param>
        public static void WriteError(string msg)
        {
            ConsoleColor actColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n{msg}");
            Console.ForegroundColor = actColor;
        }

        #endregion

        #region Properties & Members

        private static readonly string dbPath = @"..\Database\SQLLiteExample.sqlite";

        #endregion

        static void Main(string[] args)
        {
            string? error = null;
            string? er = null;

            SQLiteHelper.DBPath = dbPath;

            if (SQLiteHelper.CreateDataBaseIfNotExists(out error, dbPath) && string.IsNullOrEmpty(error))
            {
                SqliteConnection? dbConnection = SQLiteHelper.CreateConnection(out er, dbPath);
                error += er;

                if (null != dbConnection)
                {
                    if (!DBProvisioning.CreateTables(out er, dbConnection))
                    {
                        error += er;

                        if (string.IsNullOrEmpty(error))
                        {
                            WriteError("Error during Provisioning of Database!\n");
                        }
                        else
                        {
                            WriteError(error);
                        }
                        return;
                    }

                    // Create new Person
                    Person person = new Person()
                    {
                        FirstName = "Jonny",
                        LastName = "Johnson",
                        Age = 30
                    };

                    if (person.InsertPerson(out er, dbConnection))
                    {
                        // Select person
                        ReadData(dbConnection);
                    }
                    else
                    {
                        WriteError("Person was not created!\n");
                    }
                }
                else
                {
                    WriteError($"No database connection created!\n" +
                               $"{error}\n");
                }
            }
            else 
            {
                WriteError(error);
            }
            
            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }

        #region Functions

        #region DB-Functions



        #endregion

        #endregion

        static void ReadData(SqliteConnection conn)
        {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM tblPerson";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                Console.WriteLine($"\n---------------------------------------------------");
                Console.WriteLine($"Id: \"{sqlite_datareader.GetString(0)}\"");
                Console.WriteLine($"FirstName: \"{sqlite_datareader.GetString(1)}\"");
                Console.WriteLine($"LastName: \"{sqlite_datareader.GetString(2)}\"");
                Console.WriteLine($"Age: \"{sqlite_datareader.GetString(3)}\"");
                Console.WriteLine($"---------------------------------------------------\n");
            }
            conn.Close();
        }
    }
}