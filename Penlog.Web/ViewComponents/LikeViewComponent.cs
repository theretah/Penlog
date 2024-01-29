using Microsoft.AspNetCore.Mvc;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.ViewComponents
{
    public class LikeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int postId, string userId)
        {
            var like = new Like { PostId = postId, UserId = userId };
            return View(like);
        }
    }
}
