using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using CodeGeneration.Domain.Models;

namespace CodeGeneration.Models.User
{
    public class UserDashboardVM
    {
        public UserDashboardVM()
        {

        }

        public UserDashboardVM(int userId)
        {
            IUserRepository userRepo = Bootstrapper.UnityContainer.Resolve<IUserRepository>();
            UserService userService = new UserService(userRepo);
            this.Usermodel = userService.GetUser(userId);
        }

        public UserModel Usermodel { get; set; }
    }
}