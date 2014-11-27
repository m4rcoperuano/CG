using CodeGeneration.Domain.ServiceInterfaces;
using CodeGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using Microsoft.Practices.Unity;

namespace CodeGeneration.Membership
{
    public class CGMembership : IMembership
    {
        public int Login(string userName, string password, bool persistLogin = false)
        {
            int userId = 0;
            WebSecurity.Login(userName, password, persistLogin);
            userId = WebSecurity.GetUserId(userName);
            return userId;
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public int GetUserId()
        {
            return WebSecurity.CurrentUserId;
        }

        public bool IsLoggedIn()
        {
            return WebSecurity.IsAuthenticated;
        }

        public void RegisterUser(string email, string password, IEmailService emailService, IUrlBuilder urlBuilder)
        {
            string confirmationString = WebSecurity.CreateAccount(email, password, false);
            this.SendNewRegistrationEmail(email, confirmationString, emailService, urlBuilder);
        }

        private void SendNewRegistrationEmail(string email, string confirmation, IEmailService emailService, IUrlBuilder urlBuilder)
        {
            string websiteName = ConfigurationManager.AppSettings["WebsiteName"];
            string subject = "Welcome To " + websiteName + "!";
            string url = urlBuilder.CreateFullyQualifiedUrl("Confirm", "User", new { c = confirmation }, "http");
            string body = @"Welcome,

Your account has been successfully created. Please click on the following link to confirm your account: 
<a href='" + url + @"'>Confirm Your Account</a>

Thank you,
You can contact me if you have any issues at [SUPPORT EMAIL HERE]
";
            ISystemClock sysClock = Bootstrapper.UnityContainer.Resolve<ISystemClock>();
            emailService.Send(email, subject, body, sysClock.RightNow());
        }



        public bool Confirm(string confirmation)
        {
            return WebSecurity.ConfirmAccount(confirmation);
        }
    }
}