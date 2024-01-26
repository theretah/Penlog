using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penlog.Entities;

namespace Penlog.Data.Mapping
{
    public class UserCategoryMapping : IEntityTypeConfiguration<UserCategory>
    {
        public void Configure(EntityTypeBuilder<UserCategory> builder)
        {
            builder
               .HasKey(pc => new { pc.CategoryId, pc.UserId });

            builder
                .HasOne(pc => pc.Category)
                .WithMany(c => c.Followers)
                .HasForeignKey(pc => pc.CategoryId);

            builder
                 .HasOne(pc => pc.User)
                .WithMany(p => p.FavoriteCategories)
                .HasForeignKey(pc => pc.UserId);
        }
    }

}
