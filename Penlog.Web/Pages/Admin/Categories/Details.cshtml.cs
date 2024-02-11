using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Pages.Admin.Categories
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public DetailsModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public Category Category { get; set; }
        public IEnumerable<PostCategory> Posts { get; set; }

        public void OnGet(int categoryId)
        {
            Category = unit.Categories.Get(categoryId);
            Posts = unit.PostCategories.Find(pc => pc.CategoryId == categoryId);
        }
    }
}
