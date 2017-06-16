using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public class TrackUploadViewM
    {
        public int TrackId { get; set; }
        public int AlbumId { get; set; }
        public string NameOfTrack { get; set; }
        public int Position { get; set; }
        public string TrackLink { get; set; }
        public HttpPostedFileBase file { get; set; }
        public Track ConvertToTrack()
        {
            Track track = new Track();
            track.TrackId = TrackId;
            track.AlbumId = AlbumId;
            track.NameOfTrack = NameOfTrack;
            return track;
        }
    }
}