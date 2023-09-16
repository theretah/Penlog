using Microsoft.AspNetCore.Authorization;
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

        public EditModel(IUnitOfWork unit)
        {
            this.unit = unit;
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

            unit.Posts.Update(post);
            unit.Complete();

            return RedirectToPage("Index");
        }
    }
}
