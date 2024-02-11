using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Penlog.Pages.Admin.Roles
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public EditModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [BindProperty]
        public IdentityRole Role { get; set; }

        public async Task<IActionResult> OnGet(string roleId)
        {
            Role = await roleManager.FindByIdAsync(roleId);
            return Page();
        }

        public async Task<IActionResult> OnPost(string roleId)
        {
            if (!ModelState.IsValid)
                return Page();

            var role = await roleManager.FindByIdAsync(roleId);
            role.Name = Role.Name;
            await roleManager.UpdateAsync(role);

            return RedirectToPage("Index");
        }
    }
}
