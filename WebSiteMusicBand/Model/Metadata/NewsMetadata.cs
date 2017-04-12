using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMusicBand.Model
{
    public class NewsMetadata
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Title")]
        [StringLength(100, MinimumLength = 20, ErrorMessage = "Tittle must be 3 to 100 char")]
        [Required(ErrorMessage = "The title is required")]
        public string Title { get; set; }

        [DisplayName("Content")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Required(ErrorMessage = "The content is required")]
        public string TextContent { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public System.DateTime CreateDate { get; set; }

        [DisplayName("Autor")]
        public int UserId { get; set; }

        [ScaffoldColumn(false)]
        public int NewsSectionId { get; set; }

    }
}