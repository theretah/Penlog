using Microsoft.AspNetCore.Mvc;
using Penlog.Entities;

namespace Penlog.ViewComponents
{
    public class FollowFromProfileViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string followerId, string followingId)
        {
            var followModel = new Follow { FollowerId = followerId, FollowingId = followingId };
            return View(followModel);
        }
    }
}
