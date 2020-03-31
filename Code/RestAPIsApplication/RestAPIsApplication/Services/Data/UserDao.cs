using RestAPIsApplication.Models;
using RestAPIsApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RestAPIsApplication.Services.Data
{
    public class UserDao
    {
        /// <summary>
        ///     Modular method that privately store the database's connection string and consturct a Dao service's SqlConnection.
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
        ///     Modular method used to private construct a Dao service's SqlCommand.
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
        ///     Called on to check if the given user form paramaters are already in use by another user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> int result (OR throws an Exception) </returns>
        public bool UserExists(UserModel user)
        {
            bool result = false;
            try
            {
                // Setup SELECT query with parameters
                string query = "SELECT * FROM [dbo].[User] WHERE Username = @Username OR Email = @Email";

                // Create SQL connection and command
                using (SqlConnection cn = GetConn(query))
                using (SqlCommand cmd = GetComm(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = user.Username;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;

                    // Open the connection
                    cn.Open();

                    // Uses DataReader to read query results for xisting users.
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read == null)
                    {
                        result = false;
                    }
                    else
                        result = read.HasRows;

                    cn.Close();
                }
                return result;
            }
            catch (SqlException SqlEx)
            {
                // TODO: should log exception and then throw a custom exception
                Console.WriteLine(SqlEx);
                throw SqlEx;
            }
        }

        /// <summary>
        ///     Called on to pull all users from the database
        /// </summary>
        /// <returns> List<UserModel> users </UserModel> </returns>
        public List<UserModel> FindAllUsers()
        {
            // Instanciates a list for all playlists.
            List<UserModel> users = new List<UserModel>();

            // Attempts to connect to the database and pull all users from the database into a list.
            try
            {
                string query = "SELECT * FROM [dbo].[User]";
                // Create connection and command
                using (SqlConnection cn = GetConn(query))
                using (SqlCommand cmd = GetComm(query, cn))
                {
                    // Open the connection
                    cn.Open();

                    // Attempts to utilize a DataReader to pull all users from the database.
                    try
                    {
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            if (read.HasRows)
                            {
                                DateTime parsedDate;

                                // Attempts to parse the birth date value from the database.
                                if (DateTime.TryParse(read["BirthDate"].ToString(), out DateTime parseDate))
                                {
                                    parsedDate = parseDate;
                                }
                                // If it cannot be parsed, an exception is thrown.
                                else
                                {
                                    throw new UserParseException(read["BirthDate"].ToString());
                                }

                                // Adds the given user model to the list of users.
                                users.Add(new UserModel(int.Parse(read["Id"].ToString()),
                                                                  read["Username"].ToString(),
                                                                  "Password", "Email",
                                                                  int.Parse(read["Role"].ToString()),
                                                                  parsedDate));
                            }
                        }
                    }
                    // Catches all exceptions that may occur in this process.
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
                    finally
                    {
                        cn.Close();
                    }
                    return users;
                }
            }
            catch (Exception Ex)
            {
                // TODO: should log exception and then throw a custom exception
                Console.WriteLine(Ex);
                throw Ex;
            }
        }

        /// <summary>
        ///     Called to make the checks nessesary for logging a user into the site and pulling their information.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> Usermodel user </returns>
        public UserModel LoginUser(UserModel user)
        {
            // Creates a query that will select any users with the given username/email address and password.
            string query = "SELECT * FROM [dbo].[User] WHERE Username = @Username AND Password = @Password OR Email = @Email AND Password = @Password";

            // Attempts to connect to the database and run the query, pulling the data for the application and user session.
            try
            {
                // Create connection and command
                using (SqlConnection cn = GetConn(query))
                using (SqlCommand cmd = GetComm(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = user.Username;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Username;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = user.Password;

                    // Open the connection
                    cn.Open();
                    // Attempts to utilize a data reader to read and parse the user data into a user model.
                    try
                    {
                        SqlDataReader read = cmd.ExecuteReader();
                        // While reading, checks for any user data and pulls the given information.
                        while(read.Read())
                        {
                            if(read.HasRows)
                            {
                                user.Id = int.Parse(read["Id"].ToString());
                                user.Username = read["Username"].ToString();
                                user.Password = read["Password"].ToString();
                                user.Email = read["Email"].ToString();
                                user.Role = int.Parse(read["Role"].ToString());

                                // Attempts to parse the birth date value from the database.
                                if(!long.TryParse(read["BirthDate"].ToString(), out long parseDate))
                                    user.BirthDate = new DateTime(parseDate);
                                else
                                {
                                    throw new UserParseException(read["BirthDate"].ToString());
                                }
                            }
                        }
                    }
                    // Catches any Exceptions that may occur during the process.
                    catch (Exception e)
                    {
                        Console.WriteLine("Error processing user data.");
                        throw e;
                    }
                    // Closes the connection before returning the usermodel.
                    finally
                    {
                        cn.Close();
                    }
                    return user;
                }
            }
            catch (SqlException SqlEx)
            {
                // TODO: should log exception and then throw a custom exception
                Console.WriteLine(SqlEx);
                throw SqlEx;
            }
            catch (Exception Ex)
            {
                // TODO: should log exception and then throw a custom exception
                Console.WriteLine(Ex);
                throw Ex;
            }
        }

        /// <summary>
        ///     Called on to register a new user to the database/site.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> int result (OR SqlException is thrown) </returns>
        public int RegisterUser(UserModel user)
        {
            // Creates a query that will insert the user into the database.
            string query = "INSERT INTO [dbo].[User] VALUES(@Username,@Password,@Email,@Role,@BirthDate)";

            // Attempts to connect to the database and run the query, registering the user by adding them to the database.
            try
            {
                // Create connection and command
                using (SqlConnection cn = GetConn(query))
                using (SqlCommand cmd = GetComm(query, cn))
                {
                    cmd.Parameters.AddWithValue("Username", user.Username);
                    cmd.Parameters.AddWithValue("Password", user.Password);
                    cmd.Parameters.AddWithValue("Email", user.Email);
                    cmd.Parameters.AddWithValue("Role", false);
                    cmd.Parameters.AddWithValue("BirthDate", user.BirthDate);

                    // Open the connection
                    cn.Open();

                    // Runs the query and parses the result. Closes the connection and then returns the result.
                    int result = cmd.ExecuteNonQuery();
                    cn.Close();
                    return result;
                }
            }
            // Catches all exceptions that may occur during this process.
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