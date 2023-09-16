using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Penlog.Pages.Admin.Roles
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IEnumerable<IdentityRole> Roles { get; set; }
        [BindProperty]
        public string RoleName { get; set; }
        public void OnGet()
        {
            Roles = roleManager.Roles;
        }
        public async Task<IActionResult> OnPostCreate()
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(RoleName));
                if (result.Succeeded)
                {
                    return RedirectToPage("Index");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role != null)
                await roleManager.DeleteAsync(role);
            return RedirectToPage("Index");
        }
    }
}
