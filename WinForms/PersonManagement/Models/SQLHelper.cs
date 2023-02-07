using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonManagement.Models
{
    public class SQLHelper
    {
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Creates an connection string using parameters.
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="dbServer">DataSource (SQL-Servername)</param>
        /// <param name="dbName">DB-Name</param>
        /// <param name="userName">User-Name</param>
        /// <param name="password">User-Password</param>
        /// <returns>Created Connectionstring/NULL</returns>
        public static string CreateConnectionStringFromObject(out string error, string dbServer, string dbName, string userName, string password)
        {
            error = null;
            bool validation = true;
            if (string.IsNullOrEmpty(dbServer))
            {
                error += $"CreateConnecitonStringFromObject-Error:\r\n" +
                         $"DataSource is NULL or EMPTY!\r\n\r\n";
                validation = false;
            }
            if (string.IsNullOrEmpty(dbName))
            {
                error += $"CreateConnecitonStringFromObject-Error:\r\n" +
                         $"InitialCatalog is NULL or EMPTY!\r\n\r\n";
                validation = false;
            }

            if (validation)
            {
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = dbServer,
                    InitialCatalog = dbName
                };
                //string connectionString = $"Server={dbServer};Database={dbName}";

                if (!string.IsNullOrEmpty(userName))
                {
                    //connectionString += $";user={userName}";
                    connectionStringBuilder.UserID = userName;
                    if (!string.IsNullOrEmpty(password))
                    {
                        connectionStringBuilder.Password = password;
                        //connectionString += $";password={password}";
                    }
                }
                else
                {
                    connectionStringBuilder.IntegratedSecurity = true;
                }

                //var providerName = "System.Data.SqlClient";

                ConnectionString = connectionStringBuilder.ConnectionString;
                return connectionStringBuilder.ConnectionString;

                //ConnectionString = "providerName=\"System.Data.SqlClient\";Data Source=LOCALHOST;Initial Catalog=dbPerson;Integrated Security=True";
                //return "providerName=\"System.Data.SqlClient\";Data Source=LOCALHOST;Initial Catalog=dbPerson;Integrated Security=True";

                //ConnectionString = connectionString;
                //return connectionString;
            }

            return null;
        }

        /// <summary>
        /// Create db-connection to the sqlite db
        /// </summary>
        /// <param name="connectionString">DB-Connectionstring</param>
        /// <returns>SQL-Connection/NULL</returns>
        public static SqlConnection CreateConnection(out string error, string connectionString)
        {
            error = null;

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = ConnectionString;
            }

            SqlConnection sqlConn = null;

            try
            {
                // Create a new database connection:
                sqlConn = new SqlConnection(connectionString);

                //// Open the connection:
                //sqlConn.Open();
                return sqlConn;
            }
            catch (Exception ex)
            {
                error += $"Error during creation of DB-Connection!\r\n{ex.Message}\r\n";
            }


            return sqlConn;
        }

        /// <summary>
        /// Executes SQLite-Command like INSERT, UPDATE, ...
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="connectionString">Database connectionstring</param>
        /// <param name="commandString">Command as text</param>
        /// <returns>Command successfully executed => TRUE/Command not executed, or Error => FALSE</returns>
        public static bool ExecuteCommand(out string error, ref string connectionString, string commandString)
        {
            error = null;
            string er = null;

            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = ConnectionString;
                }

                using (SqlConnection sqlConnection = CreateConnection(out er, ConnectionString))
                {
                    error += er;

                    if (null == sqlConnection)
                    {
                        if (string.IsNullOrEmpty(error))
                        {
                            error += $"Cannot create DB-Connection!\r\n";
                        }

                        return false;
                    }

                    if (string.IsNullOrEmpty(commandString))
                    {
                        error += "SQLHelper.ExecuteCommand-Error:\r\nCommand-String is NULL or EMPTY!\r\n";
                        return false;
                    }

                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        // Open DB-Connection
                        sqlConnection.Open();
                    }

                    SqlCommand command = new SqlCommand();
                    command = sqlConnection.CreateCommand();
                    command.CommandText = commandString;
                    command.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"SQLHelper.ExecuteCommand-Error:\r\n" +
                         $"------------------\r\n" +
                         $"COMMAND:\r\n" +
                         $"{commandString}\r\n" +
                         $"------------------\r\n\r\n" +
                         $"### ERROR ###:\r\n{ex.Message}\r\n";
            }

            return false;
        }


    }
}
