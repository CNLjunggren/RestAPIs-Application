using Newtonsoft.Json;
using RestAPIsApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace RestAPIsApplication.Services.Data
{
    /// <summary>
    ///     Credit to Yogi S. from Code Project for code functionality in regards to:
    ///     - Creating a request to the OpenWeather API via HttpWebRequest & HttpWebResponse.
    ///     - Using StreamReader to read the resulting API response and convert it into a string to parse.
    ///     Source: https://www.codeproject.com/Articles/1180283/How-to-Implement-OpenWeatherMap-API-in-ASP-NET-MVC
    /// </summary>
    public class ApiOWDao
    {
        // Instanciates a Weather Model variable for use in this DAO service class.
        private WeatherModel weather;

        /// <summary>
        ///     Called upon by other methods to construct the correct HttpWebRequest for their appropiate call. Currently only supports the "Current Weather" api.
        /// </summary>
        /// <param name="location"></param>
        /// <returns> string apiRequest </returns>
        private HttpWebRequest GetRequestCurr(LocationModel location)
        {
            // Instantiaction of the OpenWeathers api key used for the application and other variables for the unit format.
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
        ///     Takes the location model from the business service and attempts to create a HttpWebRequest, call the api, read the api's reponse, and then return 
        ///     the result as a string for now.
        /// </summary>
        /// <param name="location"></param>
        /// <returns> string apiResponse </returns>
        public WeatherModel GetCurrent(LocationModel location)
        {
            // Instanciates the apiResponse variable to be returned.
            string apiResponse = "";

            /* Attempts to create an api request call by calling the GetRequestCurr, then uses the resulting HttpWebRequest to call the api and attempt to read its 
             * response. Once the API's response has been read, the weather model is set to a deserialized version of the API response by utilizing Newtonsoft's
             * JsonConvert method (Newtonsoft.Json package). */
            try
            {
                // Creates a Http web request based on the api call for the current weather.
                HttpWebRequest apiRequest = GetRequestCurr(location);

                // Uses the generated HttpWebResponse to read the OpenWeather API response and save it to a variable, then closes the reader.
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                    reader.Close();
                }
                //Sets the instanciated weather model equal to the deserialized API response string.
                weather = JsonConvert.DeserializeObject<WeatherModel>(apiResponse);
            }
            // Catches WebException errors and throws it upwards to the OpenWeather Controller class for handling.
            catch (WebException webExcept)
            {
                throw webExcept;
            }
            return weather;
        }
    }
}