using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebSiteMusicBand.Model.Metadata;


namespace WebSiteMusicBand.Model
{
    [MetadataType(typeof(NewsMetadata))]
    public partial class News
    {
    }

    [MetadataType(typeof(CommentsMetadata))]
    public partial class NewsComments
    {
    }

    [MetadataType(typeof(CustomUserMetadata))]
    public partial class CustomUsers
    {
    }

    [MetadataType(typeof(AlbumsMetadata))]
    public partial class Album
    {
    }
}