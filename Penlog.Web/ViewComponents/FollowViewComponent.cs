﻿using Microsoft.AspNetCore.Mvc;
using Penlog.Entities;
using Penlog.ViewModels;

namespace Penlog.ViewComponents
{
    public class FollowViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int postId, string followerId, string followingId)
        {
            var followModel = new FollowVM { FollowerId = followerId, FollowingId = followingId, PostId = postId };
            return View(followModel);
        }
    }
}
