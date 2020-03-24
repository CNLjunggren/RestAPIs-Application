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

        public List<UserModel> FindAllUsers()
        {
            // Instanciates a list for all playlists.
            List<UserModel> users = new List<UserModel>();

            // 
            try
            {
                string query = "SELECT * FROM [dbo].[User]";
                // Create connection and command
                using (SqlConnection cn = GetConn(query))
                using (SqlCommand cmd = GetComm(query, cn))
                {
                    // Open the connection
                    cn.Open();

                    // Utilizes a DataReader to pull all users from the database.
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
                                else
                                {
                                    throw new UserParseException(read["BirthDate"].ToString());
                                }

                                users.Add(new UserModel(int.Parse(read["Id"].ToString()),
                                                                  read["Username"].ToString(),
                                                                  "Password", "Email", 
                                                                  int.Parse(read["Role"].ToString()),
                                                                  parsedDate));
                            }
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
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel LoginUser(UserModel user)
        {
            string query = "SELECT * FROM [dbo].[User] WHERE Username = @Username AND Password = @Password OR Email = @Email AND Password = @Password";
            //string query = "SELECT * FROM [dbo].[User] WHERE Username = @Username AND Password = @Password";

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

                    try
                    {
                        SqlDataReader read = cmd.ExecuteReader();
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
                    catch (Exception e)
                    {
                        Console.WriteLine("Error processing user data.");
                        throw e;
                    }
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
            string query = "INSERT INTO [dbo].[User] VALUES(@Username,@Password,@Email,@Role,@BirthDate)";

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

                    int result = cmd.ExecuteNonQuery();
                    cn.Close();
                    return result;
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
    }
}