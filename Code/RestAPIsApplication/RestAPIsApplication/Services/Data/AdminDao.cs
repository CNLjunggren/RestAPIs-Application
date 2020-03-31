using RestAPIsApplication.Models;
using RestAPIsApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestAPIsApplication.Services.Data
{
    public class AdminDao
    {
        /// <summary>
        ///     Modular method used to privately store the database's connection string and consturct a Dao service's SqlConnection.
        /// </summary>
        /// <param name="query"></param>
        /// <returns> cn (SqlConnection) </returns>
        private SqlConnection GetConn(string query)
        {
            SqlConnection cn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RestAPIsDatabase;Integrated Security=True;Connect Timeout=30;" +
            "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return cn;
        }

        /// <summary>
        ///     Modular method used to private consturct a Dao service's SqlCommand.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cn"></param>
        /// <returns> cmd (SqlCommand) </returns>
        private SqlCommand GetComm(string query, SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            return cmd;
        }

        /// <summary>
        ///     Called to make a database conenction and delete the given user from the database or throw an exception.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns> int result (OR) Exception is thrown </returns>
        public int DeleteUser(int userID)
        {
            // Instanciates the query for the database.
            string query = "DELETE FROM [dbo].[User] WHERE Id = @Id";

            // Attempts to open a connection to the database and delete the user.
            try
            {
                // Create connection and command
                using (SqlConnection cn = GetConn(query))
                using (SqlCommand cmd = GetComm(query, cn))
                {
                    cmd.Parameters.AddWithValue("Id", userID);

                    // Open the connection
                    cn.Open();

                    // Gets the result of the query, closes the connection, and returns the result.
                    int result = cmd.ExecuteNonQuery();
                    cn.Close();
                    return result;
                }
            }
            // Catches exceptions that may occur in this process.
            catch (SqlException SqlEx)
            {
                Console.WriteLine(SqlEx);
                throw SqlEx;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                throw Ex;
            }
        }
    }
}