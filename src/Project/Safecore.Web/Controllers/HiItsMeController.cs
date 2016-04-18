using Sitecore;
using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Safecore.Web.Controllers
{
    public class HiItsMeController : Controller
    {
        // GET: HiItsMe
        public ActionResult HiItsMe()
        {
            var name = String.Empty;
            var location = String.Empty;
            if (Tracker.Current != null && Tracker.IsActive && Context.User.IsAuthenticated)
            {
                var contact = Tracker.Current.Contact;
                var data = contact.GetFacet<IContactPersonalInfo>("Personal");
                if (data != null && !String.IsNullOrEmpty(data.FirstName) && !String.IsNullOrEmpty(data.Surname))
                    name = String.Format("{0} {1}", data.FirstName, data.Surname);

                IAddress primaryAddress = null;
                string addressType = "primary";
                var addresses = Tracker.Current.Contact.GetFacet<IContactAddresses>("Addresses");
                if (addresses.Entries.Contains(addressType))
                {
                    primaryAddress = addresses.Entries[addressType];
                    location = primaryAddress.Country;
                }

            }
            ViewBag.Name = name;
            ViewBag.UserName = Sitecore.Context.User.Identity.Name;
            ViewBag.Location = location;
            return View();
        }
    }
}