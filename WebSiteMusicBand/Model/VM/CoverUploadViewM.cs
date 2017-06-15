using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public class CoverUploadViewM
    {
        public int AlbumId { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}