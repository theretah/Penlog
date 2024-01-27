using Penlog.Entities;

namespace Penlog.ViewModels
{
    public class CommentVM
    {
        public Comment Comment { get; set; }
        public string CommentText { get; set; }
        public IEnumerable<Comment> Replies { get; set; }
    }
}
