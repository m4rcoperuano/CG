using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeGeneration.Models.User
{
    public class RegisterVM
    {
        [Required]
        [Display(Name="Email / Username")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [Display(Name="Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        public int Registration(IUserRepository userRepository)
        {
            UserService userService = new UserService(userRepository);
            //TODO Registration Logic
            //....
            //END TODO

            Domain.Models.UserModel user = new Domain.Models.UserModel();
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Password = this.Password;
            user.Email = this.Email;

            int userId = userService.SaveUser(user);
            return userId;
        }
    }
}