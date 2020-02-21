namespace RestAPIsApplication.Models
{
    public class WeatherModel
    {
        // Instanciation & getters/setters for each variable in the location model.This also will contain data validation rules for each variable in the future.
        // Tester method for now until the chosen parameters are decided on for this app. (Each is found within the response currently).
        
        // Weather: Type
        public int WeatherTypeID { get; set; }
        public string WeatherType { get; set; }
        public string WeatherDesc { get; set; }
        public string WeatherIcon { get; set; }
        // Weather: Main Data Types
        public double Temp { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        // Weather: Wind
        public double WindSpeed { get; set; }
        public double WindDegree { get; set; }
        // Weather: Clouds
        public long CloudCover { get; set; }
        // Weather: Rain
        public long Rain1Hour { get; set; }
        public long Rain3Hours { get; set; }
        // Weather: Snow
        public long Snow1Hour { get; set; }
        public long Snow3Hours { get; set; }
        // Weather: Location
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public long Timezone { get; set; }
        public long Date { get; set; }
        public long CoordsLon { get; set; }
        public long CoordsLat { get; set; }
        // Weather: Etc.
        public long Sunrise { get; set; }
        public long Sunset { get; set; }

        /// <summary>
        ///     Default Constructor for this model class. Temporarily added for testing purposes.
        /// </summary>
        public WeatherModel()
        {
            // Weather: Type
            this.WeatherTypeID = 0;
            this.WeatherType = "";
            this.WeatherDesc = "";
            this.WeatherIcon = "";
            // Weather: Main Data Types
            this.Temp = 0;
            this.Pressure = 0;
            this.Humidity = 0;
            this.TempMin = 0;
            this.TempMax = 0;
            // Weather: Wind
            this.WindSpeed = 0;
            this.WindDegree = 0;
            // Weather: Clouds
            this.CloudCover = 0;
            // Weather: Rain
            this.Rain1Hour = 0;
            this.Rain3Hours = 0;
            // Weather: Snow
            this.Snow1Hour = 0;
            this.Snow3Hours = 0;
            // Weather: Location
            this.CityName = "";
            this.CountryName = "";
            this.Timezone = 0;
            this.Date = 0;
            this.CoordsLon = 0;
            this.CoordsLat = 0;
            // Weather: Etc.
            this.Sunrise = 0;
            this.Sunset = 0;
        }

        /// <summary>
        ///     Overloaded constructor for the weather model for future implementation when the api response is converted to a weather model.
        /// </summary>
        public WeatherModel(int WTypeID, string WType, string WDesc, string WIcon, double Temp, double Press, double Humid, double TempL, double TempH,
            double WindSpd, double WindDeg, long Clouds, long Rain1, long Rain3, long Snow1, long Snow3, string City, string Country, long Timezone, long Date,
            long CoordsLn, long CoordsLt, long Sunr, long Suns)
        {
            // Weather: Type
            this.WeatherTypeID = WTypeID;
            this.WeatherType = WType;
            this.WeatherDesc = WDesc;
            this.WeatherIcon = WIcon;
            // Weather: Main Data Types
            this.Temp = 0;
            this.Pressure = 0;
            this.Humidity = 0;
            this.TempMin = 0;
            this.TempMax = 0;
            // Weather: Wind
            this.WindSpeed = 0;
            this.WindDegree = 0;
            // Weather: Clouds
            this.CloudCover = 0;
            // Weather: Rain
            this.Rain1Hour = 0;
            this.Rain3Hours = 0;
            // Weather: Snow
            this.Snow1Hour = 0;
            this.Snow3Hours = 0;
            // Weather: Location
            this.CityName = "";
            this.CountryName = "";
            this.Timezone = 0;
            this.Date = 0;
            this.CoordsLon = 0;
            this.CoordsLat = 0;
            // Weather: Etc.
            this.Sunrise = 0;
            this.Sunset = 0;
        }
    }
}