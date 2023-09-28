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

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Author = userManager.GetUserAsync(User).Result;
            Author.PostsCount++;

            Post.CreatedDate = DateTimeOffset.Now;
            Post.Author = Author;

            var user = userManager.GetUserAsync(User).Result;

            unit.Posts.Add(Post);
            unit.Complete();

            return RedirectToPage("../Users/Profile/", new { id = user.Id });
        }
    }
}
