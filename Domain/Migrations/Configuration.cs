namespace Domain.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Entities;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Concrete;
    using Helpers;

    public sealed class Configuration : DbMigrationsConfiguration<Domain.Concrete.TwitterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Domain.Concrete.TwitterContext";
        } 

        public void FillWithData()
        {
            TwitterContext tc = new TwitterContext();
            Seed(tc);
        }

        protected override void Seed(Domain.Concrete.TwitterContext context)
        {
            InitialCreate create = new InitialCreate();
            create.Down();
            create.Up();
            context.Configuration.LazyLoadingEnabled = true;
            
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);  
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };
                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "nick@gmail.com"))
            {
                var store = new UserStore<TwitterUser>(context);
                var manager = new UserManager<TwitterUser>(store);
                var user = new TwitterUser
                {
                    Email = "nick@gmail.com",
                    EmailConfirmed = true,
                    Name = "Nick",
                    Surname = "White",
                    UserName = "nick@gmail.com",
                };
                manager.Create(user, "sssss1");
                PhotoFolderHelper.CreatePhotoFolderIfNotExist("nick@gmail.com");
            }

            if (!context.Users.Any(u => u.UserName == "gio@gmail.com"))
            {
                var store = new UserStore<TwitterUser>(context);
                var manager = new UserManager<TwitterUser>(store);
                var user = new TwitterUser
                {
                    Email = "gio@gmail.com",
                    EmailConfirmed = true,
                    Name = "Gio",
                    Surname = "Grey",
                    UserName = "gio@gmail.com",
                };
                manager.Create(user, "sssss1");
                PhotoFolderHelper.CreatePhotoFolderIfNotExist("gio@gmail.com");
            }

            if (!context.Users.Any(u => u.UserName == "david@gmail.com"))
            {
                var store = new UserStore<TwitterUser>(context);
                var manager = new UserManager<TwitterUser>(store);
                var user = new TwitterUser
                {
                    Email = "david@gmail.com",
                    EmailConfirmed = true,
                    Name = "David",
                    Surname = "Black",
                    UserName = "david@gmail.com",
                };
                manager.Create(user, "sssss1");
                PhotoFolderHelper.CreatePhotoFolderIfNotExist("david@gmail.com");
            }

            if (!context.Users.Any(u => u.UserName == "alex@gmail.com"))
            {
                var store = new UserStore<TwitterUser>(context);
                var manager = new UserManager<TwitterUser>(store);
                var user = new TwitterUser
                {
                    Email = "alex@gmail.com",
                    EmailConfirmed = true,
                    Name = "Alex",
                    Surname = "Red",
                    UserName = "alex@gmail.com",
                };
                manager.Create(user, "sssss1");
                PhotoFolderHelper.CreatePhotoFolderIfNotExist("alex@gmail.com");
            }
        }
    }
}
