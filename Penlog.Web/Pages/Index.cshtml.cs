using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public IndexModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public IEnumerable<Post> Posts { get; set; }
        public void OnGet()
        {
            Posts = unit.Posts.GetAllWithAuthors().OrderByDescending(p => p.LastUpdated);
        }
    }
}