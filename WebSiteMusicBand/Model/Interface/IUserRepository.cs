using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMusicBand.Model
{
    public interface IUserRepository
    {
        /// <summary>
        /// Return current user Id
        /// </summary>
        int CurrentIdentityID { get; }
        /// <summary>
        /// Return current identity user
        /// </summary>
        //ApplicationUser CurrentIdentityUser { get; }
        /// <summary>
        ///  Return current custom user
        /// </summary>
        //CustomUsers GetCurrenCustomtUser { get; }
        /// <summary>
        /// Return all Users
        /// </summary>
        IEnumerable<CustomUsers> GetAllUsers { get; } //?? VD Do I need this?
        /// <summary>
        /// Get custom user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CustomUsers GetCustomUserById(int id);
        /// <summary>
        /// Change custom user
        /// </summary>
        /// <param name="user"></param>
        bool EditCustomUser(CustomUsers user);
        /// <summary>
        /// Change avatar 
        /// </summary>
        /// <param name="path"></param>
        bool UpdateAvatar(string path);
    }
}
