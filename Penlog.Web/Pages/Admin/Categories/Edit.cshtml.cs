using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using Penlog.Model.Entities;

namespace Penlog.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public EditModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        [BindProperty]
        public IFormFile File { get; set; }
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = unit.Categories.GetWithPicture(id);
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var category = unit.Categories.GetWithPicture(id);

            var ms = new MemoryStream();
            File.CopyTo(ms);

            category.Picture = new Image
            {
                Bytes = ms.ToArray(),
                FileExtension = Path.GetExtension(File.FileName),
                Description = File.FileName,
                Size = File.Length
            };
            category.Description = Category.Description;
            category.Title = Category.Title;

            unit.Categories.Update(category);
            unit.Complete();

            return RedirectToPage("Index");
        }
    }
}
