using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace WebSiteMusicBand.Model
{
    public static class SecureCustomHelper
    {
        public static bool IsThisCurrentUserOrAdmin(CustomUsers user)
        {
            if (user.Id == HttpContext.Current.User.Identity.GetUserId<int>()  || HttpContext.Current.User.IsInRole("admin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsThisCurrentUserOrAdmin(int userId)
        {
            if (userId == HttpContext.Current.User.Identity.GetUserId<int>() || HttpContext.Current.User.IsInRole("admin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsCurrentUser(CustomUsers userId)
        {
            if (userId.Id == HttpContext.Current.User.Identity.GetUserId<int>() )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsCurrenAdmin()
        {
            if (HttpContext.Current.User.IsInRole("admin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsUserAdmin(int userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var role = (from r in db.Roles where r.Name.Contains("admin") select r).FirstOrDefault();
            var users = db.Users.Where(n => n.Roles == role).Where(x => x.Id == userId);

            if (users != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static CustomUsers GetCurrentCustomUser()
        {
            MusicBandSiteDB db = new MusicBandSiteDB(); //!? VD
            return db.CustomUsers.Find(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        public static int GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId<int>();
        }
    }
}