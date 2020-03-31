using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestAPIsApplication.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A username must be between 5-30 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A password must be between 5-30 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "An email must be between 6-30 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        public int Role { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        ///     Default Constructor for this model class. Temporarily added for testing purposes.
        /// </summary>
        public UserModel()
        {
            this.Username = "Default Constructor Username";
            this.Password = "Default Constructor Password";
            this.Email = "Default Constructor Email";
            this.Role = 0;
            this.BirthDate = new DateTime();
        }

        /// <summary>
        ///      Overloaded constructor for the user model for when a user is attempting to log in.
        /// </summary>
        public UserModel(string UserOrEmail, string Pass)
        {
            this.Username = UserOrEmail;
            this.Password = Pass;
            this.Email = UserOrEmail;
            this.Role = 0;
            this.BirthDate = new DateTime();
        }

        /// <summary>
        ///      Overloaded constructor for the user model for when a user is being registered.
        /// </summary>
        public UserModel(string User, string Pass, string Email, int Role, DateTime Date)
        {
            this.Username = User;
            this.Password = Pass;
            this.Email = Email;
            this.Role = Role;
            this.BirthDate = Date;
        }

        /// <summary>
        ///      Overloaded constructor for the user model for when an id is needed.
        /// </summary>
        public UserModel(int Id, string User, string Pass, string Email, int Role, DateTime Date)
        {
            this.Id = Id;
            this.Username = User;
            this.Password = Pass;
            this.Email = Email;
            this.Role = Role;
            this.BirthDate = Date;
        }
    }
}