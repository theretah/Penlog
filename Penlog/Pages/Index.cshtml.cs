using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public IndexModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public IList<Post> Posts { get; set; }
        public void OnGet()
        {
            Posts = unit.Posts
                .GetAllWithAuthors()
                .OrderByDescending(p => (p.LastUpdated == null) ? p.CreatedDate : p.LastUpdated)
                .ToList();
        }
    }
}