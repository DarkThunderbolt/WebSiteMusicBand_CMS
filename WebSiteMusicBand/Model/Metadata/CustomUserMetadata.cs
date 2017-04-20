using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model.Metadata
{
    public class CustomUserMetadata
    {

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2 to 100 char")]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2 to 100 char")]
        public string LastName { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2 to 100 char")]
        [Required(ErrorMessage = "Forum name is required")]
        public string ForumName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
    }
}