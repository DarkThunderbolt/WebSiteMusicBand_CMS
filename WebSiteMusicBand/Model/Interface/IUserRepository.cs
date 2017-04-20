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
        int CurrentIdentityID { get; }
        ApplicationUser CurrentIdentityUser { get; }
        CustomUsers GetCurrenCustomtUser { get; }
        IEnumerable<CustomUsers> GetAllUsers { get; }
        CustomUsers GetCustomUserById(int id);
        void EditCustomUser(CustomUsers user);
        void UpdateAvatar(string path);
    }
}
