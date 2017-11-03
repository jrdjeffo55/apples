using System;
using System.Collections.Generic;

namespace apple.Models
{
    public  class Idea : BaseEntity
    {
        public Idea()
        {
            Likes = new List<Like>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public List<Like> Likes { get; set; }
    }
}