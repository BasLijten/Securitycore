using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evilcore.Web.Models
{
  public class CommentModel
  {
    public int ID { get; set; }
    public string SessionID { get; set; }
    [AllowHtml]
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public string UserIdentifier { get; set; }
    public string Status { get; set; }
  }
}