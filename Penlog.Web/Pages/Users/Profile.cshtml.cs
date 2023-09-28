using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using NuGet.Configuration;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;
using Penlog.PageModels;
using System.Security.Claims;

namespace Penlog.Pages.Users
{
    public class ProfileModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;
        private readonly IFollowPageControls followPageControls;

        public ProfileModel(IUnitOfWork unit, UserManager<AppUser> userManager, IFollowPageControls followPageControls)
        {
            this.unit = unit;
            this.userManager = userManager;
            this.followPageControls = followPageControls;
        }

        public IFormFile File { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public AppUser Author { get; set; }
        public bool IsFollowing { get; set; }
        public string ProfileImageDataUrl { get; set; }

        public IActionResult OnGet(string id)
        {
            Author = userManager.FindByIdAsync(id).Result;
            var user = userManager.GetUserAsync(User).Result;
            Posts = unit.Posts.Find(p => p.AuthorId == Author.Id);

            if (user != null)
            {
                var follow = followPageControls.GetFollowEntity(user.Id, id);
                IsFollowing = unit.Follows
                    .Find(f => f.FollowerId == follow.FollowerId && f.FollowingId == follow.FollowingId)
                        .FirstOrDefault() != null;
            }

            if (Author.ProfilePhoto == null)
                ProfileImageDataUrl = "default-profile.jpg";

            OnPostRetrieveProfilePhoto();

            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            var post = unit.Posts.Get(id);
            unit.Posts.Remove(post);
            var user = userManager.GetUserAsync(User).Result;
            user.PostsCount--;

            unit.Complete();

            return OnGet(user.Id);
        }
        public IActionResult OnPostFollow(string authorId, string followerId, string followingId)
        {
            followPageControls.Follow(followerId, followingId);
            return OnGet(authorId);
        }
        public IActionResult OnPostUnFollow(string authorId, string followerId, string followingId)
        {
            followPageControls.UnFollow(followerId, followingId);
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

            var newPhoto = new Photo
            {
                User = user,
                UserId = user.Id,
                Bytes = ms.ToArray(),
                FileExtension = Path.GetExtension(File.FileName),
                Description = file.FileName,
                Size = file.Length
            };

            ms.Close();
            ms.Dispose();

            var currentPhoto = unit.Photos.Find(p => p.UserId == user.Id).FirstOrDefault();
            if (currentPhoto != null)
                unit.Photos.Remove(currentPhoto);

            user.ProfilePhoto = newPhoto;
            unit.Photos.Add(newPhoto);
            unit.Complete();

            return RedirectToPage();
        }
        public void OnPostRetrieveProfilePhoto()
        {
            var photo = unit.Photos.Find(p => p.UserId == Author.Id).SingleOrDefault();
            if (photo != null)
            {
                string imageBase64Data = Convert.ToBase64String(photo.Bytes);
                ProfileImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }
        }
    }
}
