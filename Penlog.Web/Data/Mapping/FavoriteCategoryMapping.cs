using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penlog.Entities;

namespace Penlog.Data.Mapping
{
    public class FavoriteCategoryMapping : IEntityTypeConfiguration<FavoriteCategory>
    {
        public void Configure(EntityTypeBuilder<FavoriteCategory> builder)
        {
            builder
               .HasKey(fc => new { fc.CategoryId, fc.UserId });

            builder
                .HasOne(fc => fc.Category)
                .WithMany(c => c.FavoriteFollowers)
                .HasForeignKey(fc => fc.CategoryId);

            builder
                .HasOne(fc => fc.User)
                .WithMany(p => p.FavoriteCategories)
                .HasForeignKey(fc => fc.UserId);
        }
    }
}
