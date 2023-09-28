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

        public void OnGet(int id)
        {
            Post = unit.Posts.Get(id);
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var post = unit.Posts.Get(id);
            post.Title = Post.Title;
            post.Content = Post.Content;
            post.LastUpdated = DateTimeOffset.Now;

            var user = userManager.GetUserAsync(User).Result;

            unit.Posts.Update(post);
            unit.Complete();

            return RedirectToPage("../Users/Profile/", new { id = user.Id });
        }
    }
}
