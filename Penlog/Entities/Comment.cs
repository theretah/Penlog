using Penlog.Entities;

namespace Penlog.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public string AuthorId { get; set; }
        public AppUser Author { get; set; }

        public string Content { get; set; }
        public DateTimeOffset PublishDate { get; set; }

        public int? ParentId { get; set; }
        public Comment? Parent { get; set; }
        public IEnumerable<Comment>? Replies { get; set; }
    }
}
