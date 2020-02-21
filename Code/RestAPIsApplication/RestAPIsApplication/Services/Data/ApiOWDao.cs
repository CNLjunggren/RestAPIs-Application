using RestAPIsApplication.Models;
using System;
using System.IO;
using System.Net;

namespace RestAPIsApplication.Services.Data
{
    /// <summary>
    ///     Credit to Yogi S. from Code Project for code functionality relating to creating a request to the OpenWeather API through HttpWebRequest & HttpWebResponse.
    ///     Source: https://www.codeproject.com/Articles/1180283/How-to-Implement-OpenWeatherMap-API-in-ASP-NET-MVC
    /// </summary>
    public class ApiOWDao
    {
        /// <summary>
        ///     A method called upon by other methods to construct the correct HttpWebRequest for theit appropiate call. Currently only supports the "Current
        ///     Weather" api.
        /// </summary>
        /// <param name="location"></param>
        /// <returns> string apiRequest </returns>
        private HttpWebRequest GetRequestCurr(LocationModel location)
        {
            // Instantiaction of the OpenWeathers api key used for the application and a string variable for the appropiate measureing system to be used.
            string apiKey = "3a98eaf5333fd5b9ff4e579e89ab1d7d";
            string unitsCall;

            // Checks which measurement system was selected by the user to set up the appropiate string to be used in the HttpWebRequest.
            if (location.Units == 0)
                unitsCall = "&units=imperial";
            else if (location.Units == 1)
                unitsCall = "&units=metric";
            else
                unitsCall = "";

            // Checks if the state field has been used in this request and builds the HttpWeRequest appropiately based on this, then returns the request.
            if (location.State == "")
            {
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=" + location.City + "," + location.Country +
                    location.Units + "&appid=" + apiKey) as HttpWebRequest;
                return apiRequest;
            }
            else
            {
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=" + location.City + "," + location.State + "," +
                    location.Country + unitsCall + "&appid=" + apiKey) as HttpWebRequest;
                return apiRequest;
            }
        }

        /// <summary>
        ///     DAO method that takes takes in location model from the business service and attempts to create a HttpWebRequest, call the api, read the api's
        ///     reponse, and then return the result as a string for now.
        /// </summary>
        /// </summary>
        /// <param name="location"></param>
        /// <returns> string apiResponse </returns>
        public string GetCurrent(LocationModel location)
        {
            // Instanciates the apiResponse variable to be returned.
            string apiResponse = "";

            // Attempts to create an api request call by calling another method and then using this HttpWebRequest to call the api and attempt to read its 
            // response.
            try
            {
                // Creates a Http web request based on the api call for the current weather.
                HttpWebRequest apiRequest = GetRequestCurr(location);

                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();

                    reader.Close();
                }
            }
            // Catches WebException errors and converts the api response to the appropiate error message. Will be changed in the future. 
            catch (WebException webExcept)
            {
                Console.WriteLine(webExcept);
                apiResponse = "webExcept 404";
            }
            // Catches and throws all other errors as needed.
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            return apiResponse;
        }
    }
}