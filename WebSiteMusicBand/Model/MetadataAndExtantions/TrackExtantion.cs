using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public partial class Track
    {
        public HttpPostedFile file { get; set; }

        public string Duration
        {
            get
            {                
                try
                {
                    string str = System.Web.Hosting.HostingEnvironment.MapPath("~/" + this.TrackLink);
                    TagLib.File f = TagLib.File.Create(str, TagLib.ReadStyle.Average);
                    TimeSpan time = f.Properties.Duration;
                    return ((time < TimeSpan.Zero) ? "-" : "") + time.ToString(@"mm\:ss");
                }
                catch(Exception e)
                {
                    //!V error
                    return "00:00";
                }

            }
        }

        public int GridPosition { get; set; }

        public bool DoesTrackFileExists
        {
            get
            {
                return File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/" + this.TrackLink));
            }
        }
    }
}