using RestAPIsApplication.Models;
using RestAPIsApplication.Services.Business;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;

namespace RestAPIsApplication.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        // Instanciates the user service class.
        private readonly UserService service = new UserService();

        /// <summary>
        ///     Controller method that returns the Login page to the user.
        /// </summary>
        /// <returns> View(); </returns>
        [HttpGet]
        //[AllowAnonymous]
        public ActionResult Login()
        {
            EnsureLogout();
            return View();
        }

        /// <summary>
        ///     Controller method 
        /// </summary>
        /// <returns> View(); </returns>
        [HttpPost]
        //[AllowAnonymous]
        public ActionResult LoginUser(UserLoginModel userL)
        {
            // Validates the registration form's POST contents for invalid data.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "User");
            }

            EnsureLogout();

            // Attempts to call the business service in order for the application to attempt login checks.
            try
            {
                UserModel user = new UserModel
                {
                    Username = userL.Username,
                    Password = userL.Password,
                    Email = userL.Username
                };

                bool userExists = service.UserExists(user);
                if (userExists == true)
                {
                    user = service.LoginUser(user);
                    if (user.Id > 0)
                    {
                        // Creates a Session for the user on successful login.
                        Session["UserId"] = user.Id.ToString();
                        Session["Username"] = user.Username.ToString();
                        Session["Role"] = user.Role.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Error"] = "No user with the given Username and/or Password exists.";
                        return RedirectToAction("Login", "User");
                    }
                }
                else
                {
                    TempData["Error"] = "No user with the given Username and/or Password exists.";
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e is SqlException)
                {
                    TempData["Error"] = "An unexpected error occured while attempting to register. Please try again";
                }
                else
                    TempData["Error"] = "Error: " + e + ".";
                return RedirectToAction("Login", "User");
            }
        }

        /// <summary>
        ///     Credit to Ankit Kanojia from C# Corner for code functionality in regards to:
        ///     - Creating the MVC.Net user login token functionality.
        ///     - Creating the MVC.Net logout functionality.
        ///     Source: https://www.c-sharpcorner.com/article/custom-login-functionality-in-mvc/
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     Called upon to ensure a user is logged out before logging in.
        /// </summary>
        private void EnsureLogout()
        {
            if(Session["UserID"] != null)
                Logout();
        }

        /// <summary>
        ///     Controller method that returns the Registration page to the user.
        /// </summary>
        /// <returns> View(); </returns>
        [HttpGet]
        //[AllowAnonymous]
        public ActionResult Register()
        {
            EnsureLogout();
            return View();
        }

        /// <summary>
        ///     Controller method 
        /// </summary>
        /// <returns> View(); </returns>
        [HttpPost]
        //[AllowAnonymous]
        public ActionResult RegisterUser(UserModel user)
        {
            // Validates the registration form's POST contents for invalid data.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register", "User");
            }

            EnsureLogout();

            // Attempts to call the business service in order for the application to call the database and attempt to register a new user.
            try
            {
                bool userExists = service.UserExists(user);

                if(userExists == false)
                {
                    try
                    {
                        if (service.RegisterUser(user) > 0)
                        {
                            // If no exceptions are thrown then the new user is registered to the site, and the user is directed to the login page  or a response page.
                            TempData["Error"] = "You have been sucessfully registered!";
                            return RedirectToAction("Login", "User");
                        }
                        else
                        {
                            TempData["Error"] = "An unknown error has occured. Please contact a site administrator and try again later.";
                            return RedirectToAction("Register", "User");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        if (e is SqlException)
                        {
                            TempData["Error"] = "An unexpected error occured while attempting to register. Please try again";
                        }
                        else
                            TempData["Error"] = "Error: " + e + ".";
                        return RedirectToAction("Register", "User");
                    }
                }
                else
                {
                    // If the user already exists, user is directed to registration page and shown an error message.
                    TempData["Error"] = "This username or email address is already in use.";
                    return RedirectToAction("Register", "User");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e is SqlException)
                {
                    TempData["Error"] = "An unexpected error occured while attempting to register. Please try again";
                }
                else
                    TempData["Error"] = "Error: " + e + ".";
                return RedirectToAction("Register", "User");
            }
        }
    }
}