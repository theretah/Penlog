using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;
using System.Data;

namespace Penlog.Web.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public IndexModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public IEnumerable<AppUser> Users { get; set; }
        public void OnGet()
        {
            Users = unit.Users.GetAll();
        }
    }
}
