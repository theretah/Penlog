using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Pages.Users
{
    public class EditBiographyModel : PageModel
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unit;

        public EditBiographyModel(UserManager<AppUser> userManager, IUnitOfWork unit)
        {
            this.userManager = userManager;
            this.unit = unit;
        }

        [BindProperty]
        public string? Biography { get; set; }

        public AppUser Author { get; set; }

        public void OnGet(string authorId)
        {
            Biography = userManager.FindByIdAsync(authorId).Result.Biography;
        }

        public IActionResult OnPost(string authorId)
        {
            Author = userManager.FindByIdAsync(authorId).Result;
            Author.Biography = Biography;
            unit.Complete();

            return RedirectToPage("Profile", new { id = Author.Id });
        }
    }
}
