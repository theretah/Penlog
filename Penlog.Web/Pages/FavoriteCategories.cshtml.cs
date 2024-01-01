using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Pages
{
    public class FavoriteCategoriesModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public FavoriteCategoriesModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public IEnumerable<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = unit.Categories.GetAll();
        }
    }
}
