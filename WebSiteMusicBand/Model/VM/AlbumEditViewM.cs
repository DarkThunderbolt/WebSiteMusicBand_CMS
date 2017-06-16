using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public class AlbumEditViewM
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int NumOfTracks { get; set; }
        public HttpPostedFileBase file { get; set; }
        public String CoverPath { get; set; }
        public Album ConvertToAlbum()
        {
            Album alb = new Album();
            alb.AlbumId = AlbumId;
            alb.CoverLink = CoverPath;
            alb.NumOfTracks = NumOfTracks;
            alb.Year = Year;
            alb.Title = Title;
            return alb;
        }
    }
}