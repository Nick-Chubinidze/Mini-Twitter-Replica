using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class TweetRepository : BaseRepository<Tweet>
    {
        public TweetRepository(IDbContextTwitter t) : base(t)
        {
        }
    }

    public class UserRepository : BaseRepository<TwitterUser>
    {
        public UserRepository(IDbContextTwitter t) : base(t)
        { 
        }
    }

    public class PhotoRepository : BaseRepository<Photo>
    {
        public PhotoRepository(IDbContextTwitter t) : base(t)
        {
        }
    }

    public class FollowerUserRepository : BaseRepository<FollowerUser>
    {
        public FollowerUserRepository(IDbContextTwitter t) : base(t)    
        {
        }
    }
}
