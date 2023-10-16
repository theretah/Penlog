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
        public string PreviewImageDataUrl { get; set; }
        public bool IsFollowing { get; set; }
        public bool HasLiked { get; set; }

        [BindProperty]
        public bool RefreshFlag { get; set; }
        [BindProperty]
        public string CommentTitle { get; set; }
        [BindProperty]
        public string CommentText { get; set; }

        public Post Post { get; set; }
        public AppUser CurrentUser { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public async Task OnGet(int id)
        {
            ModelState.Clear();
            CommentText = string.Empty;
            CommentTitle = string.Empty;

            Post = unit.Posts.GetWithAuthor(id);
            Comments = unit.Comments.GetWithAuthors(c => c.PostId == Post.Id);

            CurrentUser = await userManager.GetUserAsync(User);
            if (CurrentUser != null)
            {
                IsFollowing = unit.Follows
                    .Find(f => f.FollowerId == CurrentUser.Id && f.FollowingId == Post.AuthorId)
                        .FirstOrDefault() != null;

                var like = unit.Likes.Find(l => l.PostId == Post.Id && l.UserId == CurrentUser.Id).FirstOrDefault();
                HasLiked = like != null;
            }

            var previewImage = unit.Images.Find(p => p.Id == Post.PreviewImageId).SingleOrDefault();
            if (previewImage != null)
            {
                string imageBase64Data = Convert.ToBase64String(previewImage.Bytes);
                PreviewImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }
        }
        public async Task<IActionResult> OnPostLike(int postId)
        {
            Post = unit.Posts.GetWithAuthor(postId);
            CurrentUser = await userManager.GetUserAsync(User);

            var like = unit.Likes.Find(l => l.PostId == postId && l.UserId == CurrentUser.Id).FirstOrDefault();
            if (like == null)
            {
                unit.Likes.Add(new Like { Post = Post, PostId = postId, User = CurrentUser, UserId = CurrentUser.Id });
            }
            else
            {
                unit.Likes.Remove(like);
            }
            unit.Complete();

            return RedirectToPage(postId);
        }
        public IActionResult OnPostFollow(int postId, string followerId, string followingId)
        {
            followPageControls.Follow(followerId, followingId);
            return RedirectToPage(postId);
        }
        public IActionResult OnPostUnFollow(int postId, string followerId, string followingId)
        {
            followPageControls.UnFollow(followerId, followingId);
            return RedirectToPage(postId);
        }
        public IActionResult OnPostComment(int postId)
        {
            var userId = userManager.GetUserId(User);
            unit.Comments.Add(new Comment
            {
                AuthorId = userId,
                Title = CommentTitle,
                Content = CommentText,
                PublishDate = DateTimeOffset.Now,
                ParentId = null,
                PostId = postId
            });
            unit.Complete();

            return RedirectToPage(postId);
        }
        public IActionResult OnPostReplyComment(int postId, int parentId)
        {
            var userId = userManager.GetUserId(User);
            unit.Comments.Add(new Comment
            {
                AuthorId = userId,
                Title = CommentTitle,
                Content = CommentText,
                PublishDate = DateTimeOffset.Now,
                ParentId = parentId,
                PostId = postId
            });
            unit.Complete();

            return RedirectToPage(postId);
        }
        public IActionResult OnPostDeleteComment(int postId, int commentId)
        {
            var comment = unit.Comments.Get(commentId);
            unit.Comments.Remove(comment);
            unit.Complete();

            return RedirectToPage(postId);
        }
    }
}
