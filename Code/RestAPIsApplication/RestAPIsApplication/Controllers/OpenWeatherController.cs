using RestAPIsApplication.Models;
using RestAPIsApplication.Services.Business;
using System;
using System.Net;
using System.Web.Mvc;

namespace RestAPIsApplication.Controllers
{
    //[Authorize]
    public class OpenWeatherController : Controller
    {
        // Instanciates the OpenWeather api service class.
        private readonly ApiOWService service = new ApiOWService();

        /// <summary>
        ///     Controller method that returns the Current Weather request page to the user.
        /// </summary>
        /// <returns> View() </returns>
        [HttpGet]
        public ActionResult CurrentWeather()
        {
            // Checks if the user is logged in.
            if (Session["UserID"] != null)
                return View();
            else
                return RedirectToAction("Login", "User");
        }

        /// <summary>
        ///     ActionResult HttpPost method that takes the entered information from the user's current weather request and;
        ///     - Checks if the data is valid.
        ///         - If the data is valid, calls the OpenWeathers api service class and passes the location data for the api call to use.
        ///         - Else, redirects the user to the same page so error messages are shown to the user.
        ///     - Instanciates the OpenWeather API service class and calls the CallCurrent method within this service class to:
        ///         - Call the DAO Service which makes a request of weather information to the "Current Weather" API from OpenWeather.
        ///         - Return a complete model of the api's response (OR) throws a corresponding error back up to this class.
        ///     - Shows the user the Current Weather request's data (OR) displays an error message on the page and prompts the user to try again.
        /// </summary>
        /// <param name="location"></param>
        /// <returns> View(apiResponse) </returns>
        [HttpPost]
        public ActionResult CurrentResult(LocationModel location)
        {
            // Checks if the user is logged in.
            if (Session["UserID"] != null)
            {
                    /* Checks if the submitted current weather data request is invalid data. If invalid data is found, the user is redirected to the same page so error
                     * the user is shown data validation error messages. */
                    if (!ModelState.IsValid)
                    {
                        return RedirectToAction("OpenWeather", "OpenWeather");
                    }

                    /* Attempts to call the business service in order for the application to call the OpenWeather API and retrieve the results of the current
                     * location requested by the user. */
                    try
                    {
                        WeatherModel apiResponse = service.CallCurrent(location);
                        /* If no exceptions are thrown then the API's weather icon url is created using the weather response's icon #. This response weather model is then
                         * returned along the CurrentResult view. */
                        apiResponse.IconUrl = "http://openweathermap.org/img/wn/" + apiResponse.Weather[0].Icon.ToString() + ".png";
                        return View(apiResponse);
                    }
                    catch (WebException wE)
                    {
                        Console.WriteLine(wE);
                    //ViewBag.Error = "Current Weather could not be found at the given location. Please ensure you entered a valid location and try again.";
                    TempData["Error"] = "Current Weather could not be found at the given location. Please ensure you entered a valid location and try again.";
                    return RedirectToAction("CurrentWeather", "OpenWeather");
                    }
                }
            else
                return RedirectToAction("Login", "User");
        }

        /// <summary>
        ///     Controller method that returns the Weather Forcast request page to the user.
        /// </summary>
        /// <returns> View() </returns>
        [HttpGet]
        public ActionResult WeatherForecast()
        {
            // Checks if the user is logged in.
            if (Session["UserID"] != null)
                return View();
            else
                return RedirectToAction("Login", "User");
        }
    }
}