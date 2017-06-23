using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public partial class Album
    {
        public AlbumEditViewM ConvertToViewModel()
        {
            AlbumEditViewM alb = new AlbumEditViewM();
            alb.AlbumId = this.AlbumId;
            alb.CoverPath = this.CoverLink;
            alb.NumOfTracks = this.NumOfTracks;
            alb.Year = this.Year;
            alb.Title = this.Title;
            return alb;
        }
    }
}