using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Penlog.Entities;
using System.ComponentModel.DataAnnotations;

namespace Penlog.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastUpdated { get; set; }

        [ValidateNever]
        public int? PreviewImageId { get; set; }
        [ValidateNever]
        public Image? PreviewImage { get; set; }

        [ValidateNever]
        public string AuthorId { get; set; }
        [ValidateNever]
        public AppUser Author { get; set; }

        [ValidateNever]
        public IEnumerable<Like> Likes { get; set; }
        [ValidateNever]
        public IEnumerable<Comment> Comments { get; set; }
        [ValidateNever]
        public IEnumerable<PostCategory> Categories { get; set; }
    }
}
