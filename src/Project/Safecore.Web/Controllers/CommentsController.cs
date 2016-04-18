using Glass.Mapper.Sc;
using Safecore.Web.DAL;
using Sitecore.Mvc.Presentation;
using SitecoreSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Safecore.Web.Controllers
{
    public class CommentsController : Controller
    {        
        private string connectionString = @"Data Source=.\;Initial Catalog=UserInteractions;Database=UserInteractions;uid=UserInteractionDB2;pwd=User12345DB";
        private UserInteractionContext db = new UserInteractionContext();
        // GET: Comments
        public ActionResult Index(string status)
        {

            //var _status = new SqlParameter("status", status);
            var sqlQuery = "SELECT * FROM CommentModels";
            if (!String.IsNullOrEmpty(status))
                sqlQuery += " WHERE Status like @status ORDER BY Date DESC";
            else
                sqlQuery += " ORDER BY Date DESC";


            IList<CommentModel> comments = new List<CommentModel>();
            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
            {
                sqlConnection1.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection1))
                {
                    cmd.Parameters.Add("@status", SqlDbType.VarChar).Value=status;
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
            }

            return View(comments);
 //           return View(db.Comments.Select(c=>c.Status == status).ToList());
        }
        
        public ActionResult AddComment()
        {
            return View();
        }
        
        [HttpPost] 
        public ActionResult AddComment(CommentModel model)
        {
            if (ModelState.IsValid)
            {// validate model!!            
                db.Comments.Add(model);
                db.SaveChanges();
                return View();
            }

            return View();                      
        }

        public ActionResult LastComment()
        {
            // output encoding in view.
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