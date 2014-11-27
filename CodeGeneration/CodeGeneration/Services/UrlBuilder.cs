using CodeGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeGeneration.Services
{
    public class UrlBuilder : IUrlBuilder
    {
        public string CreateFullyQualifiedUrl(string action, string controller, object properties, string httpType)
        {
            string rootUrl = ConfigurationManager.AppSettings["WebsiteRootUrl"];
            //change later and use root url, for right now we are using httpcontext
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return urlHelper.Action(action, controller, properties, httpType);
        }
    }
}