using RestAPIsApplication.Models;
using RestAPIsApplication.Services.Data;
using System.Collections.Generic;
using System.Net;

namespace RestAPIsApplication.Services.Business
{
    public class ApiOWService
    {
        // Private instanciation of the Openweather api's dao class.
        private ApiOWDao dao = new ApiOWDao();

        /// <summary>
        ///     This method is called with passed down location data so it can make the appropiate dao call method to request the current weather from OpenWeather's
        ///     API. This method then returns the api's resposne back to the OpenWeather controller.
        /// </summary>
        /// <param name="location"></param>
        /// <returns> string apiResponse </returns>
        public WeatherModel CallCurrent(LocationModel location)
        {
            /* Attempts to call the GetCurrent method in the DAO data service class with the location data so the data layer of the application can request the
             * current weather the user requested. Returns the api's result in the form of a weather model. */
            try
            {
                WeatherModel apiResults = dao.GetCurrent(location);
                return apiResults;
            }
            // If a web exception is caught by the DAO data service, the exception is thrown back to the Controller to be handled.
            catch(WebException wE)
            {
                throw wE;
            }
        }
    }
}