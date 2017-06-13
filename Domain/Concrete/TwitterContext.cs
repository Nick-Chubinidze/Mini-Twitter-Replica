using System.Data.Entity;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Domain.Concrete
{
    public class TwitterContext : IdentityDbContext<TwitterUser>, IDbContextTwitter
    {
        public static TwitterContext Create()
        {
            return new TwitterContext();
        }

        public TwitterContext()
            : base("name=TwitterDBCS", throwIfV1Schema: false)
        { 
            Database.SetInitializer<TwitterContext>(new CreateDatabaseIfNotExists<TwitterContext>());
        }

        public IDbSet<Tweet> Tweets { get; set; }

        public IDbSet<Photo> Photos { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TwitterUser>().HasMany(x => x.Followers).WithMany()
                      .Map(x => x.ToTable("User_Followers"));
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        } 
    } 
}
