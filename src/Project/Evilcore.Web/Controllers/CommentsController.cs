using Evilcore.Web.DAL;
using Evilcore.Web.Models;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evilcore.Web.Controllers
{
    public class CommentsController : Controller
    {
        private string connectionString = @"Data Source=.\;Initial Catalog=UserInteractions;Database=UserInteractions;uid=sa;pwd=12345";
        private UserInteractionContext db = new UserInteractionContext();
        // GET: Comments
        public ActionResult Index(string status)
        {
            var sqlQuery = "SELECT * FROM CommentModels";
            if (!String.IsNullOrEmpty(status))
                sqlQuery += String.Format(" WHERE Status like '{0}' ORDER BY Date DESC", status);
            else
                sqlQuery += " ORDER BY Date DESC";


            IList<CommentModel> comments = new List<CommentModel>();
            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
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
        
        public ActionResult AddComment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentModel model)
        {
            IView pageView = PageContext.Current.PageView;
            if(pageView==null)
            {
                return null;
            }
            else
            {
                db.Comments.Add(model);
                db.SaveChanges();
                return View(model);
            }          
        }

        public ActionResult LastComment()
        {
            var d = db.Comments;
            var result = (from c in db.Comments
                          orderby c.ID descending
                          select c).First();                
            return View(result);
        }
    }
}