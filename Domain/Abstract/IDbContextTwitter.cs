using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation; 
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IDbContextTwitter  
    { 
        IDbSet<Tweet> Tweets { get; set; }

        IDbSet<Photo> Photos { get; set; }

        //IDbSet<FollowerUser> FollowerUsers { get; set; }    

        DbContextConfiguration Configuration { get; }

        Task<int> SaveChangesAsync(); 

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbEntityEntry Entry(object entity);

        void Dispose();
    }
}

