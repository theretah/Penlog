using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public CategoryModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public Category Category { get; set; }
        public IList<Post> Posts { get; set; } = new List<Post>();

        public void OnGet(int id)
        {
            Category = unit.Categories.Get(id);
            foreach (var pc in unit.PostCategories.Find(pc => pc.CategoryId == id).ToList())
            {
                Posts.Add(unit.Posts.GetWithAuthor(pc.PostId));
            }
        }
    }
}
