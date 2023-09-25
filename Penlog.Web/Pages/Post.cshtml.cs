using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Versioning;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;
using Penlog.PageModels;

namespace Penlog.Pages
{
    public class PostModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;
        private readonly IFollowPageControls followPageControls;

        public PostModel(IUnitOfWork unit, UserManager<AppUser> userManager, IFollowPageControls followPageControls)
        {
            this.unit = unit;
            this.userManager = userManager;
            this.followPageControls = followPageControls;
        }
        public Post Post { get; set; }
        public string ProfileImageDataUrl { get; set; }
        public bool IsFollowing { get; set; }

        public IActionResult OnGet(int id)
        {
            Post = unit.Posts.GetWithAuthor(id);

            var user = userManager.GetUserAsync(User).Result;
            if (user != null)
            {
                var follow = followPageControls.GetFollowEntity(user.Id, Post.AuthorId);
                IsFollowing = unit.Follows
                    .Find(f => f.FollowerId == follow.FollowerId && f.FollowingId == follow.FollowingId)
                        .FirstOrDefault() != null;
            }

            var photo = unit.Photos.Find(p => p.UserId == Post.AuthorId).SingleOrDefault();
            if (photo != null)
            {
                string imageBase64Data = Convert.ToBase64String(photo.Bytes);
                ProfileImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }

            return Page();
        }
        public IActionResult OnPostFollow(int postId, string followerId, string followingId)
        {
            followPageControls.Follow(followerId, followingId);
            return OnGet(postId);
        }
        public IActionResult OnPostUnFollow(int postId, string followerId, string followingId)
        {
            followPageControls.UnFollow(followerId, followingId);
            return OnGet(postId);
        }
    }
}
