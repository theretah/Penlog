using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using Penlog.Model.Entities;

namespace Penlog.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public SearchModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Post> Posts { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var posts = unit.Posts.GetAllWithAuthors()
                    .Where(g => g.Title.ToLower().Contains(SearchString.ToLower()));
                Posts = posts.ToList();

                var categories = unit.Categories.GetAll()
                    .Where(g => g.Title.ToLower().Contains(SearchString.ToLower()));
                Categories = categories.ToList();
            }
        }
    }
}
