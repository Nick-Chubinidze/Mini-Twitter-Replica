using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter_Nika_Chubinidze.Models
{
    public class MainModel
    {
        public string CoverPhoto { get; set; }

        public string ProfilePhoto { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string User { get; set; } 

        public IEnumerable<Tweet> Tweets { get; set; } 
    }

    public class FriendModel
    {
        public IEnumerable<TwitterUser> Friends { get; set; }

        public IEnumerable<TwitterUser> Users { get; set; }
    }
}