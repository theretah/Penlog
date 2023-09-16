using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Pages
{
    public class PostModel : PageModel
    {
        private readonly IUnitOfWork unit;

        public PostModel(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public Post Post { get; set; }
        public void OnGet(int id)
        {
            Post = unit.Posts.GetWithAuthor(id);
        }
    }
}
