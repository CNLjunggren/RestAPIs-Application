using RestAPIsApplication.Models;
using RestAPIsApplication.Services.Business;
using System.Web.Mvc;

namespace RestAPIsApplication.Controllers
{
    public class OpenWeatherController : Controller
    {
        // TODO: Change the CurrentResult class so it can return a WeatherModel instead of a simple string containing the api's response.

        /// <summary>
        ///     Controller method that returns the Current Weather Page to the user.
        /// </summary>
        /// <returns> View() </returns>
        [HttpGet]
        public ActionResult OpenWeather()
        {
            return View();
        }

        /// <summary>
        ///     ActionResult HttpPost method that takes the entered information from the user's current weather request and;
        ///     - Checks if the data is valid.
        ///     - If the data is valid, calls the OpenWeathers api service class and passes the location data for the api call to use.
        /// </summary>
        /// <param name="location"></param>
        /// <returns> RedirectToAction("OpenWeather", "OpenWeather") </returns>
        [HttpPost]
        public ActionResult CurrentResult(LocationModel location)
        {
            // Checks if the submitted current weather data request is invalid data. If invalid data is found, the user is redirected to the same page so error
            // the user is shown data validation error messages.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("OpenWeather", "OpenWeather");
            }

            // Instanciates the OpenWeather api service class and calls the method to set the returning api resonse to a string message.
            ApiOWService service = new ApiOWService();
            string apiResponse = service.CallCurrent(location);

            // Checks if any error messages or lack of data is found instead of the expected api response string. Temporary portion of code for when only a string
            // is returned from the api call.
            if (apiResponse != null)
            {
                // Dedicated response catch for if there was a web exception when the call was sent.
                if (apiResponse == "webExcept 404")
                {
                    apiResponse = "The entered city and/or country does not exist. Please enter a valid city/country and try again.";
                    return View((object)apiResponse);
                }
                // If no errors or null api response string was found, returns the results view to the user with the appropiate string.
                else
                    return View((object)apiResponse);
            }
            // Dedicated response catch for if the api response string returns as null.
            else
            {
                apiResponse = "No current weather data available.";
                return View((object)apiResponse);
            }
        }
    }
}