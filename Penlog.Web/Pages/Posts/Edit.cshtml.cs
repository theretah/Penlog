using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Configuration;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using Penlog.Utilities;
using Penlog.Entities;

namespace Penlog.Pages.Posts
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public EditModel(IUnitOfWork unit, UserManager<AppUser> userManager)
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

        public IList<PostCategory> PostCategories { get; set; }

        public string PreviewImageDataUrl { get; set; }

        public void OnGet(int id)
        {
            Post = unit.Posts.GetWithPreviewImage(id);
            PostCategories = unit.PostCategories.FindWithCategory(pc => pc.PostId == Post.Id).ToList();

            CategoriesSelectList = new SelectList(unit.Categories.GetAll(), nameof(Category.Id), nameof(Category.Title));

            var previewImage = unit.Images.Find(p => p.Id == Post.PreviewImageId).SingleOrDefault();
            if (previewImage != null)
            {
                PreviewImageDataUrl = ImageUtilities.GenerateImageDataUrl(previewImage.Bytes);
            }
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var post = unit.Posts.Get(id);
            var postCategories = unit.PostCategories.Find(pc => pc.PostId == id);

            var author = userManager.GetUserAsync(User).Result;

            if (File != null)
            {
                var ms = new MemoryStream();
                File.CopyTo(ms);

                post.PreviewImage = new Image
                {
                    Bytes = ms.ToArray(),
                    FileExtension = Path.GetExtension(File.FileName),
                    Description = File.FileName,
                    Size = File.Length
                };
            }
            post.Title = Post.Title;
            post.Content = Post.Content;
            post.LastUpdated = DateTimeOffset.Now;

            foreach (var postCategory in postCategories)
            {
                unit.PostCategories.Remove(postCategory);
            }
            foreach (var categoryId in SelectedCategories)
            {
                var category = unit.Categories.Get(categoryId.Value);
                unit.PostCategories.Add(new PostCategory
                {
                    Post = post,
                    Category = category
                });
            }

            unit.Posts.Update(post);
            unit.Complete();

            return RedirectToPage("../Users/Profile/", new { id = author.Id });
        }
    }
}
