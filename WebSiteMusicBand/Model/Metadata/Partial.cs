using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    [MetadataType(typeof(NewsMetadata))]
    public partial class News
    {
    }

    [MetadataType(typeof(CommentsMetadata))]
    public partial class NewsComment
    {
    }

    [MetadataType(typeof(UserInfoMetadata))]
    public partial class AspNetUser
    {
    }
}