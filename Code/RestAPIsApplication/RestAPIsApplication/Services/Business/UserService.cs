using RestAPIsApplication.Models;
using RestAPIsApplication.Services.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RestAPIsApplication.Services.Business
{
    public class UserService
    {
        // Private instanciation of the Openweather api's dao class.
        private readonly UserDao dao = new UserDao();

        /// <summary>
        ///     Called with passed down user data so it can make the appropiate dao call method to check if the user attempting to register already exists in
        ///     the user database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> string userReg </returns>
        public bool UserExists(UserModel user)
        {
            /* Attempts to call the GetCurrent method in the DAO data service class with the location data so the data layer of the application can request the
             * current weather the user requested. Returns the api's result in the form of a weather model. */
            try
            {
                Console.WriteLine(user);
                bool userExists = dao.UserExists(user);
                return userExists;
            }
            // If an exception is caught by the DAO data service, the exception is thrown back to the Controller to be handled.
            catch (SqlException SqlE)
            {
                throw SqlE;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel LoginUser(UserModel user)
        {
            return dao.LoginUser(user);
        }

        /// <summary>
        ///     Called with passed down user data so it can register the new user to the database/site.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> int result (Or throws exception) </returns>
        public int RegisterUser(UserModel user)
        {
            return dao.RegisterUser(user);
        }

        public List<UserModel> FindAllUsers()
        {
            return dao.FindAllUsers();
        }
    }
}