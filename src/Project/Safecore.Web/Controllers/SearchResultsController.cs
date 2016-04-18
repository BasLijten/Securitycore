using Shared.Models;
using SitecoreSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Safecore.Web.Controllers
{
  public class SearchResultsController:Controller
  {    
    public ActionResult Index(string q)
    {
            var term = Request.QueryString["q"];
            SearchModel model = new SearchModel();
            model.SearchTerm = term;

            var list = new List<SearchResultsModel>();

            var m1 = new SearchResultsModel { SearchTitle = "Serialize all the things with Unicorn", Description = @"Sitecore development artifacts are both code and database items, such as rendering code and rendering items. As developers, we use serialization to write our database artifacts into source control along with our code so that we have a record of all our development artifacts. Unicorn is a tool to make serializing items and sharing them across teams and environments easy and fun. <br />
                In this session we’ll go over what Unicorn is,
                why you would want it,
                demonstrate how to use Unicorn,
                and show some of the new features in the latest release." };
            list.Add(m1);
            model.SearchResults = list;
            return View(model);
    }
  }
}