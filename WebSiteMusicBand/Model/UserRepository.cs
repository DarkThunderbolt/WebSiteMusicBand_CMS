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
                return HttpContext.Current.User.Identity.GetUserId<int>();
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
                return db.CustomUsers.Find(CurrentIdentityID);
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
            return db.CustomUsers.Where(n => n.Id == id).First();
        }

        public void EditCustomUser(CustomUsers user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateAvatar(string path)
        {
            db.CustomUsers.Find(CurrentIdentityID).AvatarLink = path;
            db.Entry(GetCurrenCustomtUser).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}