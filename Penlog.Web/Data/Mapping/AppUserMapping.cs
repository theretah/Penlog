using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penlog.Entities;
using System.Reflection.Emit;

namespace Penlog.Data.Mapping
{
    public class AppUserMapping : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
             .HasMany(a => a.Posts)
             .WithOne(p => p.Author)
             .HasForeignKey(p => p.AuthorId);

            builder
               .HasMany(u => u.Comments)
               .WithOne(c => c.Author)
               .HasForeignKey(c => c.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
