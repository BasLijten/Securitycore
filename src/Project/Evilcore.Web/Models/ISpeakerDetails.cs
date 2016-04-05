using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evilcore.Web.Models
{
    [SitecoreType(AutoMap = true)]
    public interface ISpeaker: ISpeakerDetails
    {

    }
    [SitecoreType(AutoMap =true)]
    public interface ISpeakerDetails
    {        
        Guid Id { get; set; }        
        string Name { get; set; }
        string Details { get; set; }
        string Biography { get; set; }
        string Facebook { get; set; }
        string Twitter { get; set; }
        string LinkedIn { get; set; }
        Image ProfilePicture { get; set; }
        string Url { get; set; }
    }

    [SitecoreType(AutoMap = true)]
    public interface ISpeakers
    {
        Guid Id { get; set; }
        IEnumerable<ISpeakerDetails> Children { get; set; }
        string Url { get; set; }
    }
}