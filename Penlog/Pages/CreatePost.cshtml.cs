using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Pages
{
    [Authorize]
    public class CreatePostModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public CreatePostModel(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public IFormFile File { get; set; }

        [BindProperty]
        public int?[] SelectedCategories { get; set; }

        public SelectList CategoriesSelectList { get; set; }

        public IActionResult OnGet()
        {
            CategoriesSelectList = new SelectList(unit.Categories.GetAll(), nameof(Category.Id), nameof(Category.Title));

            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return OnGet();

            var author = userManager.GetUserAsync(User).Result;

            if (File != null)
            {
                var ms = new MemoryStream();
                File.CopyTo(ms);

                Post.PreviewImage = new Image
                {
                    Bytes = ms.ToArray(),
                    FileExtension = Path.GetExtension(File.FileName),
                    Description = File.FileName,
                    Size = File.Length
                };
            }
            Post.CreatedDate = DateTimeOffset.Now;
            Post.Author = author;

            author.PostsCount++;

            foreach (var categoryId in SelectedCategories)
            {
                if (categoryId == null)
                    break;

                var category = unit.Categories.Get(categoryId.Value);
                unit.PostCategories.Add(new PostCategory
                {
                    Post = Post,
                    Category = category
                });

                var uc = unit.UserCategories.Find(uc => uc.CategoryId == categoryId && uc.UserId == author.Id);
                if (uc == null)
                {
                    unit.UserCategories.Add(new UserCategory
                    {
                        User = Post.Author,
                        Category = category
                    });
                }
            }

            unit.Posts.Add(Post);
            unit.Complete();

            return RedirectToPage("../Profile/", new { id = author.Id });
        }
    }
}
