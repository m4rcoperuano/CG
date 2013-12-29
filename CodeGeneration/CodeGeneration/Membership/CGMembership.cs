using CodeGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

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
    }
}