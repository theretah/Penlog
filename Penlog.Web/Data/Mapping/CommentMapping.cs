using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penlog.Model.Entities;

namespace Penlog.Data.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
               .HasOne(c => c.Parent)
               .WithMany(p => p.Replies)
               .HasForeignKey(r => r.ParentId)
               .IsRequired(false);
        }
    }

}
