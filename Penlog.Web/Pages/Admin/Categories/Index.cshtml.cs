using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using Penlog.Model.Entities;

namespace Penlog.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public IndexModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Image> Images { get; set; }

        [BindProperty]
        public Category Category { get; set; }
        [BindProperty]
        public IFormFile File { get; set; }

        public SelectList ParentCategorySelectList { get; set; }

        public void OnGet()
        {
            Categories = unit.Categories.GetAll();
            Images = unit.Images.GetAll();
            ParentCategorySelectList = new SelectList(unit.Categories.GetAll(),
                nameof(Category.Id), nameof(Category.Title));
        }

        public IActionResult OnPostAdd()
        {
            unit.Categories.Add(Category);
            unit.Complete();

            return RedirectToPage();
        }
        public IActionResult OnPostDelete(int categoryId)
        {
            var category = unit.Categories.Get(categoryId);
            unit.Categories.Remove(category);
            unit.Complete();

            return RedirectToPage();
        }
    }
}
