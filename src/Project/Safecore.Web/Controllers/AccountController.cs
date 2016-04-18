using Sitecore;
using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.Security.Authentication;
using Sitecore.SessionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Safecore.Web.Extensions;
using System.Web.Mvc;
using SitecoreSecurity.Web.Models;

namespace Safecore.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {           
            return View();
        }

        [HttpPost]        
        public ActionResult Login(LoginInfo loginInfo)
        {
            this.ResetCurrentSession();
            var result = AuthenticationManager.Login(loginInfo.UserName, loginInfo.Password);
            return View();
        }

        private void IdentifyContact(string identifier)
        {
            var site = Context.Site;
            var page = Context.Page;
            var en = site.EnableAnalytics;
            bool IsActive = (Tracker.Current != null && Tracker.Current.IsActive);
            try
            {
                if (IsActive)
                {
                    Tracker.Current.Session.Identify(identifier);
                }
            }
            catch (ItemNotFoundException ex)
            {
                //Error can happen if previous user profile has been deleted
                Log.Error($"Could not identify the user '{identifier}'", ex, this);
            }
        }
        // not needed: manually provisioned via admin screen
        //public ActionResult Register()
        //{
        //    // do we need this? manually adding accounts?
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(RegistrationInfo registrationInfo)
        //{
        //    return View();
        //}

        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            this.ResetCurrentSession();

            return View();
        }
    }
}