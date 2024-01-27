using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using Penlog.Utilities;

namespace Penlog.Pages
{
    public class PostModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public PostModel(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }
        public string PreviewImageDataUrl { get; set; }
        public bool IsFollowing { get; set; }
        public bool HasLiked { get; set; }

        [BindProperty]
        public bool RefreshFlag { get; set; }
        [BindProperty]
        public string CommentText { get; set; }

        public Post Post { get; set; }
        public AppUser CurrentUser { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<PostCategory> PostCategories { get; set; }

        public async Task OnGet(int id)
        {
            ModelState.Clear();
            CommentText = string.Empty;

            Post = unit.Posts.GetWithAuthor(id);
            Comments = unit.Comments.GetWithAuthors(c => c.PostId == Post.Id);
            PostCategories = unit.PostCategories.FindWithCategory(pc => pc.PostId == Post.Id);
            Likes = unit.Likes.Find(l => l.PostId == Post.Id);

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
                PreviewImageDataUrl = ImageUtilities.GenerateImageDataUrl(previewImage.Bytes);
            }
        }
        public async Task<IActionResult> OnPostLike(int postId)
        {
            CurrentUser = await userManager.GetUserAsync(User);
            var userId = CurrentUser.Id;

            var like = unit.Likes.Find(l => l.PostId == postId && l.UserId == userId).FirstOrDefault();
            if (like == null)
            {
                unit.Likes.Like(postId, userId);
            }
            else
            {
                unit.Likes.UnLike(postId, userId);
            }
            unit.Complete();
            return RedirectToPage(postId);
        }
        public IActionResult OnPostFollow(int postId, string followerId, string followingId)
        {
            unit.Follows.Follow(followerId, followingId);
            unit.Complete();

            return RedirectToPage(postId);
        }
        public IActionResult OnPostUnFollow(int postId, string followerId, string followingId)
        {
            unit.Follows.UnFollow(followerId, followingId);
            unit.Complete();

            return RedirectToPage(postId);
        }
        public IActionResult OnPostComment(int postId)
        {
            var userId = userManager.GetUserId(User);
            unit.Comments.Add(new Comment
            {
                AuthorId = userId,
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

            var children = unit.Comments.Find(c => c.ParentId == commentId);
            foreach (var child in children)
            {
                unit.Comments.Remove(child);
            }
            unit.Complete();

            return RedirectToPage(postId);
        }
    }
}
