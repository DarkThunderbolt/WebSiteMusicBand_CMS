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

        public bool EditCustomUser(CustomUsers user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.logger.Info($"Custom user {user.Id} edited");
                return true;
            }
            catch
            {
                return false;
            }
       
        }

        public bool UpdateAvatar(string path)
        {
            try
            {
                int id = SecureCustomHelper.GetCurrentUserId();
                CustomUsers user = db.CustomUsers.Where(x => x.Id == id).FirstOrDefault();
                if(user==null)
                {
                    return false;
                }
                user.AvatarLink = path;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges(); 
                MvcApplication.logger.Info($"Custom user {SecureCustomHelper.GetCurrentUserId()} avatar edited");
                return true;
            }
            
            catch(Exception e)
            {
                MvcApplication.logger.Error($"Error while custom user {SecureCustomHelper.GetCurrentUserId()} avatar editing/ Ex: "+ e.ToString());
                return false;
            }
        }
    }
}