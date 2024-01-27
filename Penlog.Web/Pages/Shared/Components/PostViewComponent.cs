using Microsoft.AspNetCore.Mvc;
using Penlog.Data.Repository.IRepository;

namespace Penlog.Pages.Shared.Components
{
    public class PostViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unit;

        public PostViewComponent(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            var post = unit.Posts.Get(postId);
            return View(post);
        }
    }
}
