using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Pages.Admin.Categories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public IndexModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public IEnumerable<Category> Categories { get; set; }
        public SelectList ParentCategorySelectList { get; set; }

        [BindProperty]
        public Category Category { get; set; }

        public void OnGet()
        {
            Categories = unit.Categories.GetAll();
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
