using SitecoreSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class SearchModel
    {
        public string SearchTerm { get; set; }
        public List<SearchResultsModel> SearchResults { get; set; }
    }
}
