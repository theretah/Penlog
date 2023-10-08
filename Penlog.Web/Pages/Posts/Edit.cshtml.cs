using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Configuration;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Pages.Posts
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public EditModel(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public IFormFile File { get; set; }

        public string PreviewImageDataUrl { get; set; }

        public void OnGet(int id)
        {
            Post = unit.Posts.GetWithPreviewImage(id);
            var previewImage = unit.Images.Find(p => p.Id == Post.PreviewImageId).SingleOrDefault();
            if (previewImage != null)
            {
                string imageBase64Data = Convert.ToBase64String(previewImage.Bytes);
                PreviewImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var post = unit.Posts.Get(id);

            var author = userManager.GetUserAsync(User).Result;

            if (File != null)
            {
                var ms = new MemoryStream();
                File.CopyTo(ms);

                post.PreviewImage = new Image
                {
                    Bytes = ms.ToArray(),
                    FileExtension = Path.GetExtension(File.FileName),
                    Description = File.FileName,
                    Size = File.Length
                };
            }
            post.Title = Post.Title;
            post.Content = Post.Content;
            post.LastUpdated = DateTimeOffset.Now;
            
            unit.Posts.Update(post);
            unit.Complete();

            return RedirectToPage("../Users/Profile/", new { id = author.Id });
        }
    }
}
