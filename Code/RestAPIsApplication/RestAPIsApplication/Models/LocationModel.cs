using System.ComponentModel.DataAnnotations;

namespace RestAPIsApplication.Models
{
    /// <summary>
    ///     Model class that represents the location of a user's given current weather search.
    /// </summary>
    public class LocationModel
    {
        // Instanciation & getters/setters for each variable in the location model. This also contains data validation rules for each variable.
        [StringLength(60, MinimumLength = 1, ErrorMessage = "A city name must be no more than 60 characters.")]
        [Required]
        public string City { get; set; }

        [StringLength(60, MinimumLength = 1, ErrorMessage = "A city name must be no more than 60 characters.")]
        [Required]
        public string Country { get; set; }

        public string State { get; set; }

        [Range(0,2, ErrorMessage = "You must choose one of the following three measurements types.")]
        [Required]
        public int Units { get; set; }

        /// <summary>
        ///     Default Constructor for this model class. Temporarily added for testing purposes.
        /// </summary>
        public LocationModel()
        {
            this.City = "Default Constructor City";
            this.Country = "Default Constructor Country";
            this.State = "Default Constructor State";
            this.Units = 0;
        }

        /// <summary>
        ///     Overloaded constructor for the location model for when a state is not given by the user.
        /// </summary>
        public LocationModel(string cityInput, string countryInput, int unitsSelection)
        {
            this.City = cityInput;
            this.Country = countryInput;
            this.Units = unitsSelection;
        }

        /// <summary>
        ///     Overloaded constructor for the location model for when a state is given by the user.
        /// </summary>
        public LocationModel(string cityInput, string countryInput, string stateInput, int unitsSelection)
        {
            this.City = cityInput;
            this.Country = countryInput;
            this.State = stateInput;
            this.Units = unitsSelection;
        }
    }
}