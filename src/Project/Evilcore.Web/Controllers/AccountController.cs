﻿using Evilcore.Web.Models;
using Sitecore;
using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.Mvc.Presentation;
using Sitecore.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evilcore.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            if(Tracker.Current!=null && Tracker.Current.IsActive)
            {
                
            }
            return View();
        }

        [HttpPost]              
        public ActionResult Login(LoginInfo loginInfo)
        {            
            var result = AuthenticationManager.Login(loginInfo.UserName, loginInfo.Password);
            if(result)
            {
                IdentifyContact(loginInfo.UserName);                
            }
            
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
            Session.Abandon();
            Response.Cookies["ASP.NET_SessionId"].Value = String.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            return View();
        }       
    }
}