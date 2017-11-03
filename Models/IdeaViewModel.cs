using System;
using System.ComponentModel.DataAnnotations;

namespace apple.Models
{
    public class IdeaViewModel : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}