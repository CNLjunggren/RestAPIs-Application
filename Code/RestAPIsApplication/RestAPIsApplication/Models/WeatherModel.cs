using System.Collections.Generic;

namespace RestAPIsApplication.Models
{
    /// /// <summary>
    ///     Main weather model class for weather models that instanciates each variable and helper class variable that makes up a OpenWeather "Current 
    ///     Weather" API response.
    /// 
    ///     Credit to Yogi S. from Code Project for overall model structure for easy deserialization of OpenWeather API request results.
    ///     Source: https://www.codeproject.com/Articles/1180283/How-to-Implement-OpenWeatherMap-API-in-ASP-NET-MVC
    /// </summary>
    public class WeatherModel
    {
        public Coord Coord { get; set; }
        public List<Weather> Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public Rain Rain { get; set; }
        public Snow Snow { get; set; }
        public int Dt { get; set; }
        public Sys Sys { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
        public string IconUrl { get; set; }
    }

    /// <summary>
    ///     Helper model class that is utilized by the main class to store coordinates from the API response.
    /// </summary>
    public class Coord
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }

    /// <summary>
    ///     Helper model class that is utilized by the main class to store basic weather data from the API response.
    /// </summary>
    public class Weather
    {
        public int ID { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    /// <summary>
    ///     Helper model class that is utilized by the main class to store temperature, pressure, and humidity data from the API response.
    /// </summary>
    public class Main
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
    }

    /// <summary>
    ///     Helper model class that is utilized by the main class to store wind data from the API response.
    /// </summary>
    public class Wind
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
    }

    /// <summary>
    ///     Helper model class that is utilized by the main class to store cloud coverage data from the API response.
    /// </summary>
    public class Clouds
    {
        public int all { get; set; }
    }

    /// <summary>
    ///     Helper model class that is utilized by the main class to store rain data from the API response.
    /// </summary>
    public class Rain
    {
        public double oneH { get; set; }
        public double threeH { get; set; }
    }

    /// <summary>
    ///     Helper model class that is utilized by the main class to store snow data from the API response.
    /// </summary>
    public class Snow
    {
        public double oneH { get; set; }
        public double threeH { get; set; }
    }

    /// <summary>
    ///     Helper model class that is utilized by the main class to store location data from a requested area from the API response.
    /// </summary>
    public class Sys
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public double Message { get; set; }
        public string Country { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}