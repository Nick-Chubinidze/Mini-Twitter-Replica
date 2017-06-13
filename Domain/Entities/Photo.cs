using System.ComponentModel.DataAnnotations; 

namespace Domain.Entities
{
    public class Photo
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(250)]
        public string PhotoPath { get; set; }

        public bool IsCover { get; set; } 

        public virtual TwitterUser TwitterUser { get; set; } 
    }
}
