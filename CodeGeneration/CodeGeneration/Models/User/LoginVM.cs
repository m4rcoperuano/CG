using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Domain.Services;
using CodeGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeGeneration.Models.User
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; }

        public bool LoginUser(IUserRepository userRepo, ISystemClock time, IMembership membership)
        {
            UserService userService = new UserService(userRepo);
            int userId = membership.Login(this.Email, this.Password, this.RememberMe);

            if (userId > 0)
            {
                userService.RecordUserLogin(userId, time.RightNow());
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}