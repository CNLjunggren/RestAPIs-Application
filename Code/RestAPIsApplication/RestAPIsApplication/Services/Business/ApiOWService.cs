using RestAPIsApplication.Models;
using RestAPIsApplication.Services.Data;

namespace RestAPIsApplication.Services.Business
{
    public class ApiOWService
    {
        // Private instanciation of the Openweather api's dao class.
        private ApiOWDao dao = new ApiOWDao();

        /// <summary>
        ///     This method is called with passed down location data so it can call the appropiate dao call method for the current weather api. This method then
        ///     returns the api's resposne back to the OpenWeather controller.
        /// </summary>
        /// <param name="location"></param>
        /// <returns> string apiResponse </returns>
        public string CallCurrent(LocationModel location)
        {
            string apiResponse = dao.GetCurrent(location);
            return apiResponse;
        }
    }
}