using System; 
using System.Security.Claims;
using System.Threading.Tasks; 
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Twitter_Nika_Chubinidze.Models;
using Domain.Concrete;
using Domain.Entities;

namespace Twitter_Nika_Chubinidze
{ 
    public class ApplicationUserManager : UserManager<TwitterUser>
    {
        public ApplicationUserManager(IUserStore<TwitterUser> store)
            : base(store)
        {
            //this.UserValidator = new UserValidator<TwitterUser>(this) { AllowOnlyAlphanumericUserNames = false };
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<TwitterUser>(context.Get<TwitterContext>()));
            

            manager.UserValidator = new UserValidator<TwitterUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
             
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false,
            };
             
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(15);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
             

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<TwitterUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
     
    public class ApplicationSignInManager : SignInManager<TwitterUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(TwitterUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
