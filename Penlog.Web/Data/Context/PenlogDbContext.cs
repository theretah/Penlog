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

            modelBuilder.Entity<Follow>()
                .HasKey(f => new { f.FollowerId, f.FollowingId });

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(follower => follower.Follows)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Following)
                .WithMany()
                .HasForeignKey(f => f.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.ProfilePhoto)
                .WithOne(p => p.User)
                .HasForeignKey<Photo>(u => u.UserId);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
