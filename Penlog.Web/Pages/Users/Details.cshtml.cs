using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Web.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> usermanager;

        public DetailsModel(IUnitOfWork unit, UserManager<AppUser> usermanager)
        {
            this.unit = unit;
            this.usermanager = usermanager;
        }
        public IEnumerable<Post> Posts { get; set; }
        public AppUser Author { get; set; }
        public void OnGet(string userId)
        {
            Author = unit.Users.Get(userId);
            Posts = unit.Posts.GetAll(p => p.AuthorId == Author.Id);
        }
    }
}
