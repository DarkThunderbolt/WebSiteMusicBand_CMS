using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSiteMusicBand.Model
{
    public class CommentsMetadata
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int NewsId { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public System.DateTime CreateDate { get; set; }
        
        [DisplayName("Text")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "The content is required")]
        public string CommentsText { get; set; }
    }
}