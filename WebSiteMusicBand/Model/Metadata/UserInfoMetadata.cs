using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebSiteMusicBand.Model
{
    public class UserInfoMetadata
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public bool EmailConfirmed { get; set; }

        [ScaffoldColumn(false)]
        public string PasswordHash { get; set; }

        [ScaffoldColumn(false)]
        public string SecurityStamp { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [ScaffoldColumn(false)]
        public bool PhoneNumberConfirmed { get; set; }

        [ScaffoldColumn(false)]
        public bool TwoFactorEnabled { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }

        [ScaffoldColumn(false)]
        public bool LockoutEnabled { get; set; }

        [ScaffoldColumn(false)]
        public int AccessFailedCount { get; set; }

        [DisplayName("User name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage ="Name must be 3 to 100 char")]
        [Required(ErrorMessage = "The email address is required")]
        public string UserName { get; set; }

        [ScaffoldColumn(false)]
        public string AvatarLink { get; set; }
    }
}