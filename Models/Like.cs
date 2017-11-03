using System;
using System.Collections.Generic;

namespace apple.Models
{
    public class Like : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int IdeaId { get; set; }
        public Idea Idea { get; set; }
    }
}