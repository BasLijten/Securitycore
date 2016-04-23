using System.ComponentModel.DataAnnotations;

namespace SitecoreSecurity.Web.Models
{
    public class LoginInfo
    {

        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}