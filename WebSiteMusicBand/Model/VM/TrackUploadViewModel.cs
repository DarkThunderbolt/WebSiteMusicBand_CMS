using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public class TrackUploadViewModel
    {
        public int TrackId { get; set; }
        public HttpPostedFileBase file { get; set; }
        public int AlbumId{ get; set; }
    }
}