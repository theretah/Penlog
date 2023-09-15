using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Web.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public DetailsModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public Post Post { get; set; }
        public void OnGet(int id)
        {
            Post = unit.Posts.Get(id);
        }
    }
}
