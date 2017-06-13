using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model.Metadata
{
    public class AlbumsMetadata
    {
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be 1 to 100 char")]
        public string Title { get; set; }

        [Range(1980, 2100)] //!V
        [Required(ErrorMessage = "number of traks is required")]
        public int Year { get; set; }

        [Range(1, Int32.MaxValue)] //!V
        [Required(ErrorMessage = "number of traks is required")]
        public int NumOfTracks { get; set; }

    }
}