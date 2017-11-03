using System;
using System.Collections.Generic;

namespace apple.Models
{
    public  class User : BaseEntity
    {
        public User()
        {
            Ideas = new List<Idea>();
            Likes = new List<Like>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Idea> Ideas { get; set; }
        public List<Like> Likes { get; set; }
    }
}