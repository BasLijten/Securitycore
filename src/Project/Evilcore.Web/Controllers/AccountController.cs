using Sitecore;
using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.Mvc.Presentation;
using Sitecore.Security.Authentication;
using SitecoreSecurity.Web.Models;
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

                IdentifyContact(Sitecore.Context.User.Name);                
            }
            
            return View();
        }

        private void IdentifyContact(string identifier)
        {
            var site = Context.Site;
            var page = Context.Page;            
            bool IsActive = (Tracker.Current != null && Tracker.Current.IsActive);
            try
            {
                if (IsActive && !identifier.ToLower().Contains("anonymous"))
                {
                    Tracker.Current.Session.Identify(identifier);
                    switch (identifier.ToLower())
                    {
                        case "extranet\\robbert":
                            SetContactCard("Robbert", "Hack");
                            break;
                        case "extranet\\bas":
                            SetContactCard("Bas", "Lijten");
                            break;
                        default:
                            break;
                    }                        
                }
            }
            catch (ItemNotFoundException ex)
            {
                //Error can happen if previous user profile has been deleted
                Log.Error($"Could not identify the user '{identifier}'", ex, this);
            }
        }

        private void SetContactCard(string firstName, string lastName)
        {            
            var contact = Tracker.Current.Contact.GetFacet<IContactPersonalInfo>("Personal");
            contact.FirstName = firstName;
            contact.Surname = lastName;
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