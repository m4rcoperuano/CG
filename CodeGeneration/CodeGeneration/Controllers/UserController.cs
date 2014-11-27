using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Domain.ServiceInterfaces;
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
        private IEmailService _emailService;
        private IUrlBuilder _urlBuilder;

        public UserController(IUserRepository userRepo, ISystemClock sysClock, IMembership membership, IEmailService emailService, IUrlBuilder urlBuilder)
        {
            this._userRepo = userRepo;
            this._systemClock = sysClock;
            this._membership = membership;
            this._emailService = emailService;
            this._urlBuilder = urlBuilder;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login(string email = "")
        {
            ViewBag.RegisterSuccessMessage = TempData["RegisterSuccessMessage"];
            ViewBag.ConfirmSuccess = TempData["ConfirmSuccess"];
            ViewBag.ConfirmError = TempData["ConfirmError"];
            LoginVM loginVM = new LoginVM();
            loginVM.Email = email;

            return View(loginVM);
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
                bool success = loginVM.LoginUser(_userRepo, _systemClock, _membership);
                if (!success)
                {
                    ModelState.AddModelError("", "Credentials invalid");
                    return View(loginVM);
                }
                
                return RedirectToAction("Index", "Dashboard");
            }

            return View(loginVM);
        }

        [HttpPost]
        [CaptchaMvc.Attributes.CaptchaVerify("Captcha was incorrect. Please try again.")]
        public ActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                int result = registerVM.Registration(_userRepo);
                if (result == 0)
                {
                    ModelState.AddModelError("Email", "This email is already in use.");
                }
                else
                {
                    this._membership.RegisterUser(registerVM.Email, registerVM.Password, _emailService, _urlBuilder);
                    TempData["RegisterSuccessMessage"] = "Thank you for signing up! Login below.";
                    return RedirectToAction("Login", new { email = registerVM.Email });
                }
            }

            return View(registerVM);
        }

        public ActionResult Confirm(string c)
        {
            if (_membership.Confirm(c))
            {
                TempData["ConfirmSuccess"] = "Your account has been successfully confirmed. Please login below using your email and the password you created.";
            }
            else
            {
                TempData["ConfirmError"] = "There was an error confirming your account. Please contact us for assistance at SUPPORT EMAIL HERE";
            }

            return RedirectToAction("Login");
        }
    }
}
