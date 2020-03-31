using RestAPIsApplication.Models;
using RestAPIsApplication.Services.Business;
using RestAPIsApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestAPIsApplication.Controllers
{
    public class AdminController : Controller
    {
        // Instanciates private, readonly service and model calls for the controller.
        private readonly UserModel user = new UserModel();
        private readonly UserController userCon = new UserController();
        private readonly UserService service = new UserService();
        private readonly AdminService adminService = new AdminService();

        /// <summary>
        ///     Called upon to ensure a user is an admin before accessing parts of the website.
        /// </summary>
        /// <returns> true (OR) false </returns>
        public bool IsAdmin()
        {
            // Attempts to parse the user role value from the database and returns the appropiate value.
            if (Session["Role"].ToString() == "1")
                return true;
            else
                return false;
        }

        /// <summary>
        ///     Pulls up the user management page for users with the admin role.
        /// </summary>
        /// <returns> View() (OR) RedirectToAction() </returns>
        [HttpGet]
        public ActionResult UserList()
        {
            // Checks if the user is logged in.
            if (Session["UserID"] != null)
                // Checks if the user is an admin.
                if (IsAdmin() != false)
                {
                    // Instanciates a list of playlists that is filled by calling the FindAllPlaylists() service class.
                    List<UserModel> users = service.FindAllUsers();

                    // Instanciates and fills a Tuple of all users and the UserModel to pass to the view.
                    Tuple<List<UserModel>, UserModel> userList = new Tuple<List<UserModel>, UserModel>(users, user);
                    return View(userList);
                }
                // If the user is not an admin, user is redirected to the home page.
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            // If no user is logged in, redirects to the login page.
            else
                return RedirectToAction("Login", "User");
        }

        /// <summary>
        ///     View called from the User List page to delete or change the role of an existing user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> View() </returns>
        [HttpPost]
        public ActionResult DeleteUser(UserModel user)
        {
            return View(user);
        }

        /// <summary>
        ///     Deletes a given user from the database as long as they are not an admin.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> View() (OR) RedirectToAction() </returns>
        [HttpPost]
        public ActionResult DeletingUser(UserModel user)
        {
            // Checks if the user is logged in.
            if (Session["UserID"] != null)
                // Checks if the user is an admin.
                if (IsAdmin() != false)
                {
                    // Checks if the user being deleted exists.
                    bool userExists = service.UserExists(user);

                    // If the user exists, calls the admin services to delete the user.
                    if (userExists != false)
                    {
                        try
                        {
                            // Attempts to delete the user and gives the appropiate response/error message.
                            if (adminService.DeleteUser(user.Id) > 0)
                            {
                                // If no exceptions are thrown then the new user is registered to the site, and the user is directed to the login page  or a response page.
                                TempData["Error"] = "You have sucessfully deleted this user.";
                                return RedirectToAction("UserList", "Admin");
                            }
                            else
                            {
                                TempData["Error"] = "An unknown error has occured. Please contact a site administrator and try again later.";
                                return RedirectToAction("UserList", "Admin");
                            }
                        }
                        // Catches any exceptions from the attempt to delete the user.
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                            // Checks if exception is a SQLException and specificies how the error will be shown.
                            if (e is SqlException)
                            {
                                TempData["Error"] = "An unexpected error occured while attempting to delete the user. Please try again";
                            }
                            else
                                TempData["Error"] = "Error: " + e + ".";
                            return RedirectToAction("UserList", "Admin");
                        }
                    }
                        return View();
                }
                // If not an admin, user is redirected to home page
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            // If not logged in, user is sent to the login page.
            else
                return RedirectToAction("Login", "User");
        }
    }
}