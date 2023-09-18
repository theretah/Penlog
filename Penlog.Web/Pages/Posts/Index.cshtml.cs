using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Pages.Posts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public IndexModel(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }
        public IEnumerable<Post> Posts { get; set; }
        public AppUser Author { get; set; }
        public void OnGet()
        {
            Posts = unit.Posts.Find(p => p.AuthorId == userManager.GetUserAsync(User).Result.Id);
        }
        public IActionResult OnPostDelete(int id)
        {
            var post = unit.Posts.Get(id);
            unit.Posts.Remove(post);
            userManager.GetUserAsync(User).Result.PostsCount--;
            unit.Complete();

            return RedirectToPage("Index");
        }
    }
}
