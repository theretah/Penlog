using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penlog.Entities;

namespace Penlog.Data.Mapping
{
    public class PostCategoryMapping : IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            builder
              .HasKey(pc => new { pc.CategoryId, pc.PostId });

            builder
                .HasOne(pc => pc.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(pc => pc.CategoryId);

            builder
                .HasOne(pc => pc.Post)
                .WithMany(p => p.Categories)
                .HasForeignKey(pc => pc.PostId);
        }
    }

}
