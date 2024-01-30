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
            var postCategories = unit.PostCategories.Find(pc => pc.CategoryId == id).ToList();
            foreach (var pc in postCategories)
            {
                var post = unit.Posts.GetWithAuthor(pc.PostId);
                Posts.Add(post);
            }
        }
    }
}
