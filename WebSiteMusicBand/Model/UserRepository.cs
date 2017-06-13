using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace WebSiteMusicBand.Model
{
    public class UserRepository : IUserRepository
    {
        private MusicBandSiteDB db = new MusicBandSiteDB();

        public int CurrentIdentityID
        {
            get
            {
                return SecureCustomHelper.GetCurrentUserId();
            }

        }
        public ApplicationUser CurrentIdentityUser
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(CurrentIdentityID);
            }
        }
        public CustomUsers GetCurrenCustomtUser
        {
            get
            {
                return SecureCustomHelper.GetCurrentCustomUser();
            }
        }

        public IEnumerable<CustomUsers> GetAllUsers
        {
            get
            {
                return db.CustomUsers.ToList();
            }
        }

        public CustomUsers GetCustomUserById(int id)
        {
            return db.CustomUsers.Where(n => n.Id == id).FirstOrDefault();
        }

        public void EditCustomUser(CustomUsers user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateAvatar(string path)
        {
            CustomUsers user = db.CustomUsers.Find(CurrentIdentityID);
            user.AvatarLink = path;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}