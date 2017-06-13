using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Tweet
    {
        public Tweet()
        {
            ReTweets = new HashSet<Tweet>();
        }

        [Key]
        public int Id { get; set; }

        public string TweetText { get; set; }

        public DateTime TweetDate { get; set; } 

        public virtual TwitterUser User { get; set; }

        public virtual ICollection<Tweet>  ReTweets { get; set; }

        public bool IsComment { get; set; } 
    }
}
