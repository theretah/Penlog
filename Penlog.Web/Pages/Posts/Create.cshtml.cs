using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Pages.Posts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public CreateModel(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }

        [BindProperty]
        public Post Post { get; set; }

        public AppUser Author { get; set; }

        [BindProperty]
        public IFormFile File { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var file = Request.Form.Files.FirstOrDefault();
            var ms = new MemoryStream();
            Author = userManager.GetUserAsync(User).Result;

            file.CopyTo(ms);

            Post.PreviewImage = new Image
            {
                Bytes = ms.ToArray(),
                FileExtension = Path.GetExtension(File.FileName),
                Description = file.FileName,
                Size = file.Length
            };
            Post.CreatedDate = DateTimeOffset.Now;
            Post.Author = Author;

            Author.PostsCount++;

            unit.Posts.Add(Post);
            unit.Complete();

            return RedirectToPage("../Users/Profile/", new { id = Author.Id });
        }
    }
}
