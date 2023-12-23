using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using Penlog.Model.Entities;

namespace Penlog.Pages.Admin.Categories
{
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
            Category = unit.Categories.GetWithPicture(categoryId);
            Posts = unit.PostCategories.Find(pc => pc.CategoryId == categoryId);
        }
    }
}
