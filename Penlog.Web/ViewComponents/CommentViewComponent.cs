using Microsoft.AspNetCore.Mvc;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using Penlog.ViewModels;

namespace Penlog.ViewComponents
{
    public class CommentViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unit;

        public CommentViewComponent(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<IViewComponentResult> InvokeAsync(int commentId, string commentText, IEnumerable<Comment> replies)
        {
            var comment = unit.Comments.Get(commentId);
            return View(new CommentVM
            {
                Comment = comment,
                Replies = replies,
                CommentText = commentText
            });
        }
    }
}
