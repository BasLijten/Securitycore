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
using Sitecore.Links;
using Sitecore.Data;
using Sitecore.Mvc.Configuration;
using Sitecore.Analytics.Model.Entities;

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
            if(result)
            {
                var options = new UrlOptions
                {
                    AddAspxExtension = false,
                    LanguageEmbedding = LanguageEmbedding.Never
                };

                var pathInfo = LinkManager.GetItemUrl(Sitecore.Context.Database.GetItem(new ID("{AED2560D-4C6F-48E0-922D-CBCCCBF008D1}")), options);

                return RedirectToRoute(MvcSettings.SitecoreRouteName, new { pathInfo = pathInfo.TrimStart(new char[] { '/' }) });

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
                        case "extranet\\kam":
                            SetContactCard("Notorious", "F.I.G.");
                            break;
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
            if (firstName.ToLower() == "robbert")
            {
                SetCountry("The Netherlands");
            }

            if (firstName.ToLower() == "bas")
            {
                SetCountry("Brabant ;)");
            }

            if (firstName.ToLower() == "notorious")
            {
                SetCountry("Portland Oregon, USA");
            }

        }

        private void SetCountry(string country)
        {
            string addressType = "primary";
            var addresses = Tracker.Current.Contact.GetFacet<IContactAddresses>("Addresses");
            if (!addresses.Entries.Contains("primary"))
                addresses.Entries.Create("primary");
            var primaryAddress = addresses.Entries["primary"];
            primaryAddress.Country = country;
        }//}

        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            this.ResetCurrentSession();

            return View();
        }
    }
}