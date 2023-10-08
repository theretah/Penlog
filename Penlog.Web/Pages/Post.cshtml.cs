using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public string PreviewImageDataUrl { get; set; }
        public bool IsFollowing { get; set; }
        public bool HasLiked { get; set; }
        public AppUser AppUser { get; set; }
        [BindProperty]
        public bool RefreshFlag { get; set; }

        public IActionResult OnGet(int id)
        {
            Post = unit.Posts.GetWithAuthor(id);

            AppUser = userManager.GetUserAsync(User).Result;
            if (AppUser != null)
            {
                var follow = followPageControls.GetFollowEntity(AppUser.Id, Post.AuthorId);
                IsFollowing = unit.Follows
                    .Find(f => f.FollowerId == follow.FollowerId && f.FollowingId == follow.FollowingId)
                        .FirstOrDefault() != null;

                var like = unit.Likes.Find(l => l.PostId == Post.Id && l.UserId == AppUser.Id).FirstOrDefault();
                HasLiked = like != null;
            }

            var photo = unit.Images.Find(p => p.Id == Post.Author.ProfileImageId).SingleOrDefault();
            ProfileImageDataUrl = "default-profile.jpg";
            if (photo != null)
            {
                string imageBase64Data = Convert.ToBase64String(photo.Bytes);
                ProfileImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }

            var previewImage = unit.Images.Find(p => p.Id == Post.PreviewImageId).SingleOrDefault();
            if (previewImage != null)
            {
                string imageBase64Data = Convert.ToBase64String(previewImage.Bytes);
                PreviewImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }

            return Page();
        }
        public async Task<IActionResult> OnPostLike(int postId)
        {
            Post = unit.Posts.GetWithAuthor(postId);
            AppUser = userManager.GetUserAsync(User).Result;

            var like = unit.Likes.Find(l => l.PostId == postId && l.UserId == AppUser.Id).FirstOrDefault();
            if (like == null)
            {
                unit.Likes.Add(new Like { Post = Post, PostId = postId, User = AppUser, UserId = AppUser.Id });
            }
            else
            {
                unit.Likes.Remove(like);
            }
            unit.Complete();
            return OnGet(postId);
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
