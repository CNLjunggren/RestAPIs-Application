using RestAPIsApplication.Models;
using RestAPIsApplication.Services.Business;
using RestAPIsApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestAPIsApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserController userCon = new UserController();
        private readonly UserService service = new UserService();

        /// <summary>
        ///     Called upon to ensure a user is an admin before accessing parts of the website.
        /// </summary>
        public bool IsAdmin()
        {
            // Attempts to parse the user role value from the database.
            if (Session["Role"].ToString() == "1")
                return true;
            else
                return false;
        }

        /// <summary>
        ///     Pulls up the user management page for admins only.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserList()
        {
            // Checks if the user is logged in.
            if (Session["UserID"] != null)
                // Checks if the user is an admin.
                if (IsAdmin() != false)
                {
                    // Instanciates a way to call user models.
                    UserModel user = new UserModel();

                    // Instanciates a list of playlists that is filled by calling the FindAllPlaylists() service class.
                    List<UserModel> users = service.FindAllUsers();

                    // Instanciates and fills a Tuple of all users and the UserModel to pass to the view.
                    Tuple<List<UserModel>, UserModel> userList = new Tuple<List<UserModel>, UserModel>(users, user);
                    return View(userList);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            else
                return RedirectToAction("Login", "User");
        }
    }
}