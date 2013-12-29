using CodeGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace CodeGeneration.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static bool IsUserAuthenticated(this HtmlHelper helper)
        {
            IMembership membership = Bootstrapper.UnityContainer.Resolve<IMembership>();
            return membership.IsLoggedIn();
        }
    }
}