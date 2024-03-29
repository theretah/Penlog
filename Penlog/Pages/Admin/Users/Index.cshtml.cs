using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Entities;

namespace Penlog.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> userManager;

        public IndexModel(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IEnumerable<AppUser> Users { get; set; }
        public void OnGet()
        {
            Users = userManager.Users.ToList();
        }
    }
}
