using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Http;

namespace WebSiteMusicBand.Model
{
    public static class SecureCustomHelper
    {
        /// <summary>
        /// Do checking user is current user or in admin role
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Do current user is cheking user or not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Do current user have admin role
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Chek user for admin role by UserId 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Return current customuser
        /// </summary>
        /// <returns></returns>
        public static CustomUsers GetCurrentCustomUser()
        {
            MusicBandSiteDB db = new MusicBandSiteDB(); //!? VD 
            return db.CustomUsers.Find(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        /// <summary>
        /// Return current user id
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentUserId() //!? VD
        {
            return HttpContext.Current.User.Identity.GetUserId<int>();
        }
    }
}