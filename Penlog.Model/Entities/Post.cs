using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Penlog.Model.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastUpdated { get; set; }

        [ValidateNever]
        public string AuthorId { get; set; }
        [ValidateNever]
        public AppUser Author { get; set; }
    }
}
