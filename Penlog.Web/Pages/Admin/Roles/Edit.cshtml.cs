using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Penlog.Web.Pages.Admin.Roles
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
        public string RoleName { get; set; }
        public async void OnGet(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            RoleName = role.Name;
        }

        public async Task<IActionResult> OnPost(string roleId)
        {
            if (!ModelState.IsValid)
                return Page();

            var role = await roleManager.FindByIdAsync(roleId);
            role.Name = RoleName;
            await roleManager.UpdateAsync(role);

            return RedirectToPage("Index");
        }
    }
}
