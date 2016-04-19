using Evilcore.Web.DAL;
using Glass.Mapper.Sc;
using Sitecore.Mvc.Presentation;
using SitecoreSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evilcore.Web.Controllers
{
    public class CommentsController : Controller
    {
        private UserInteractionContext db = new UserInteractionContext();
        // GET: Comments
        public ActionResult Index(string status)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["UserInteractionContext"];
            var sqlQuery = "SELECT * FROM CommentModels";
            if (!String.IsNullOrEmpty(status))
                sqlQuery += String.Format(" WHERE Status like '{0}' ORDER BY Date DESC", status);
            else
                sqlQuery += " ORDER BY Date DESC";


            IList<CommentModel> comments = new List<CommentModel>();
            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString.ConnectionString))
            {
                sqlConnection1.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection1))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        comments.Add(new CommentModel
                        {
                            Comment = reader.GetString(2),
                            Date = reader.GetDateTime(3),
                            Status = reader.GetString(5),
                            UserIdentifier = reader.GetString(4),
                            SessionID = reader.GetString(1)
                        });
                    }
                }




                    
            }
                
            
            
            
            return View(comments);
        }

        public ActionResult AllComments(string status)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SafeUserInteractionContext"];
            var sqlQuery = "SELECT * FROM CommentModels";
            if (!String.IsNullOrEmpty(status))
                sqlQuery += String.Format(" WHERE Status like '{0}' ORDER BY Date DESC", status);
            else
                sqlQuery += " ORDER BY Date DESC";


            IList<CommentModel> comments = new List<CommentModel>();
            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString.ConnectionString))
            {
                sqlConnection1.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection1))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new CommentModel
                        {
                            Comment = reader.GetString(2),
                            Date = reader.GetDateTime(3),
                            Status = reader.GetString(5),
                            UserIdentifier = reader.GetString(4),
                            SessionID = reader.GetString(1)
                        });
                    }
                }





            }
            return View(comments);
        }

        public ActionResult AddComment()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentModel model)
        {
            var isAuthenticated = Sitecore.Context.User.IsAuthenticated;
            var context = new SitecoreContext();
            var currentItem = context.GetCurrentItem<ISessionDetail>();

            model.Status = "unapproved";
            model.Date = DateTime.Now;
            model.SessionID = currentItem.SessionID.ToString();
            if (!isAuthenticated)
                model.UserIdentifier = "Anonymous";
            else
            {
                model.UserIdentifier = Sitecore.Context.User.Name;
            }

            db.Comments.Add(model);
            db.SaveChanges();
            return View(model);                      
        }

        public ActionResult LastComment()
        {
            var d = db.Comments;
            var result = (from c in db.Comments
                          orderby c.ID descending
                          select c).First();                
            return View(result);
        }

        public ActionResult GetCommentsForSession()
        {            
            var context = new SitecoreContext();
            var currentItem = context.GetCurrentItem<ISessionDetail>();
            var comments = db.Comments.Where(c => c.SessionID == currentItem.SessionID.ToString()).ToList();

            return View(comments);
        }

        public ActionResult SPAComments()
        {
            var ctx = new SitecoreContext();
            var currentItem = ctx.GetCurrentItem<ISessionDetail>();
            var comments = db.Comments.Where(c => c.SessionID == currentItem.SessionID.ToString());
            currentItem.Comments = comments;
            return View(currentItem);
        }
    }
}