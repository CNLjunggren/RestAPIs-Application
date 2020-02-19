using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestAPIsApplication.Controllers
{
    public class OpenWeatherController : Controller
    {
        // TODO: Private instanciation of the APIService business service.
        

        /// <summary>
        ///     Current Weather Page.
        /// </summary>
        /// <returns>View()</returns>
        public ActionResult OpenWeather()
        {
            return View();
        }

        /// <summary>
        ///     Http Post method responsible for calling the business service responsible for API calls to OpenWeather in regards to Current Weather requests.
        /// </summary>
        /// <returns>View()</returns>
        public ActionResult GetCurrent()
        {
            // TODO: Add functionality to call the OpenWeather API and make a Current Weather request based off the user input.
            return RedirectToAction("OpenWeather", "OpenWeather");
        }
    }
}