using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using Penlog.Entities;

namespace Penlog.Pages.Users
{
    public class ProfileModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public ProfileModel(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }

        public IFormFile File { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public AppUser Author { get; set; }
        public bool IsFollowing { get; set; }
        public string ProfileImageDataUrl { get; set; }

        public IActionResult OnGet(string id)
        {
            Author = userManager.FindByIdAsync(id).Result;
            Posts = unit.Posts.Find(p => p.AuthorId == Author.Id);

            var user = userManager.GetUserAsync(User).Result;
            if (user != null)
            {
                IsFollowing = unit.Follows
                    .Find(f => f.FollowerId == user.Id && f.FollowingId == id)
                        .FirstOrDefault() != null;
            }

            if (Author.ProfileImage == null)
                ProfileImageDataUrl = "default-profile.jpg";

            OnPostRetrieveProfilePhoto();

            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            var post = unit.Posts.Get(id);
            unit.Posts.Remove(post);
            var comments = unit.Comments.Find(c => c.PostId == id);
            unit.Comments.RemoveRange(comments);
            var likes = unit.Likes.Find(l => l.PostId == id);
            unit.Likes.RemoveRange(likes);

            var user = userManager.GetUserAsync(User).Result;
            user.PostsCount--;

            unit.Complete();

            return OnGet(user.Id);
        }
        public IActionResult OnPostFollow(string authorId, string followerId, string followingId)
        {
            unit.Follows.Follow(followerId, followingId);
            unit.Complete();

            return OnGet(authorId);
        }
        public IActionResult OnPostUnFollow(string authorId, string followerId, string followingId)
        {
            unit.Follows.UnFollow(followerId, followingId);
            unit.Complete();

            return OnGet(authorId);
        }
        public IActionResult OnPostUploadProfilePhoto()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = userManager.GetUserAsync(User).Result;
            var file = Request.Form.Files.FirstOrDefault();
            var ms = new MemoryStream();

            file.CopyTo(ms);

            var newPhoto = new Image
            {
                Bytes = ms.ToArray(),
                FileExtension = Path.GetExtension(File.FileName),
                Description = file.FileName,
                Size = file.Length
            };

            ms.Close();
            ms.Dispose();

            var currentPhoto = unit.Images.Find(i => i.Id == user.ProfileImageId).FirstOrDefault();
            if (currentPhoto != null)
                unit.Images.Remove(currentPhoto);

            user.ProfileImage = newPhoto;
            unit.Images.Add(newPhoto);
            unit.Complete();

            return RedirectToPage();
        }
        public void OnPostRetrieveProfilePhoto()
        {
            var image = unit.Images.Find(i => i.Id == Author.ProfileImageId).FirstOrDefault();
            if (image != null)
            {
                string imageBase64Data = Convert.ToBase64String(image.Bytes);
                ProfileImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }
        }
    }
}
