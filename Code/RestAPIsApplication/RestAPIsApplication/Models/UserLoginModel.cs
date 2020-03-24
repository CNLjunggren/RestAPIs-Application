using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestAPIsApplication.Models
{
    public class UserLoginModel
    {
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A username must be between 5-30 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A password must be between 5-30 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     Default Constructor for this model class. Temporarily added for testing purposes.
        /// </summary>
        public UserLoginModel()
        {
            this.Username = "Default Constructor Username";
            this.Password = "Default Constructor Password";
        }

        /// <summary>
        ///      Overloaded constructor for the user model for when a user is attempting to log in.
        /// </summary>
        public UserLoginModel(string UserOrEmail, string Pass)
        {
            this.Username = UserOrEmail;
            this.Password = Pass;
        }
    }
}