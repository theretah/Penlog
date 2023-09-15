using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Penlog.Model.Entities;

namespace Penlog.Data.Context
{
    public class PenlogDbContext : IdentityDbContext<AppUser>
    {
        public PenlogDbContext(DbContextOptions<PenlogDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
