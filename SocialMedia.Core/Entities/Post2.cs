using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Entities
{
    public class Post2
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        //public int PostId { get; set; }


    }
}
