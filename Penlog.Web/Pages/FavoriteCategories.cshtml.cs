using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using System.Collections;

namespace Penlog.Pages
{
    public class FavoriteCategoriesModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public FavoriteCategoriesModel(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }

        public IEnumerable<Category> Categories { get; set; }

        [BindProperty]
        public IList<int> SelectedCategories { get; set; }

        public void OnGet()
        {
            Categories = unit.Categories.GetAll().OrderBy(c => c.Title);
        }
        public IActionResult OnPost()
        {
            var userId = userManager.GetUserAsync(User).Result.Id;
            foreach (var category in SelectedCategories)
            {
                unit.FavoriteCategories.Add(new FavoriteCategory { CategoryId = category, UserId = userId });
            }

            unit.Complete();
            return RedirectToPage("Index");
        }
    }
}
