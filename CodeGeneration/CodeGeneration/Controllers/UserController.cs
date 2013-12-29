using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Interfaces;
using CodeGeneration.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CodeGeneration.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepo;
        private ISystemClock _systemClock;
        private IMembership _membership;
        public UserController(IUserRepository userRepo, ISystemClock sysClock, IMembership membership)
        {
            this._userRepo = userRepo;
            this._systemClock = sysClock;
            this._membership = membership;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Logout()
        {
            this._membership.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                loginVM.LoginUser(_userRepo, _systemClock, _membership);
                return RedirectToAction("Index", "Dashboard");
            }

            return View(loginVM);
        }

        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                registerVM.Registration(_userRepo);
                WebSecurity.CreateUserAndAccount(registerVM.Email, registerVM.Password);
            }

            return View(registerVM);
        }
    }
}
