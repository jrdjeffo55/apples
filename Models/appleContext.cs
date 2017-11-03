using Microsoft.EntityFrameworkCore;

namespace apple.Models
{
    public class appleContext : DbContext
    {
        public appleContext(DbContextOptions<appleContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Like> Likes { get; set; }
        
    }
}