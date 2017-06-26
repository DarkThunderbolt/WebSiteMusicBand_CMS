using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebSiteMusicBand.Model.Metadata;

namespace WebSiteMusicBand.Model
{
    [MetadataType(typeof(AlbumsMetadata))]
    public partial class Album
    {        
        public HttpPostedFileBase file { get; set; }

    }
}