using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace SitecoreSecurity.Web.Models
{
    [SitecoreType(AutoMap = true)]
    public interface ISessionDetail
    {
        [SitecoreId]
        Guid Id { get; set; }
        [SitecoreField("Session Name")]
        string SessionTitle { get; set; }
        [SitecoreField("Session Description")]
        string SessionDescription { get; set; }

        [SitecoreField("Session Speaker")]
        ISpeakerDetails Speaker { get; set; }

        [SitecoreField("Session Speaker")]
        string SpeakerString { get; set; }
        
        [SitecoreId]
        Guid SessionID { get; set; }        
        string Url { get; set; }
    }

    [SitecoreType(AutoMap =true)]
    public interface ISessionOverview
    {
        [SitecoreId]
        Guid Id { get; set; } 
        IEnumerable<ISessionDetail> Children { get; set; }
    }
}