using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TwitterUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<TwitterUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }

        public TwitterUser()
        {
            Tweets = new HashSet<Tweet>();
            Photos = new HashSet<Photo>();
            Followers = new HashSet<TwitterUser>();
        }

        public string Name { get; set; }

        public string Surname { get; set; }
         
        public virtual ICollection<TwitterUser> Followers { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }
    }
}
