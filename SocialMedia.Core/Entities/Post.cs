using System;
using System.Collections.Generic;

namespace SocialMedia.Core.Entities
{
    public partial class Post
    {
        public Post()        //constructor (tiene mismo nombre que la clase)
        {
            Coments = new HashSet<Coment>();
        }
                
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Coment> Coments { get; set; }
    }
}
  