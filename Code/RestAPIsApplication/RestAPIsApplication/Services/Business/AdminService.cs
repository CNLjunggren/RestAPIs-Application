using RestAPIsApplication.Models;
using RestAPIsApplication.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestAPIsApplication.Services.Business
{
    public class AdminService
    {
        // Private instanciation of the Openweather api's dao class.
        private readonly AdminDao dao = new AdminDao();

        /// <summary>
        ///     Calls the Admin DAO to delete the given user from the database. Returns the result.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns> int result </returns>
        public int DeleteUser(int userID)
        {
            return dao.DeleteUser(userID);
        }
    }
}