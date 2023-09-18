using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Pages.Users
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
        public bool IsFollowing { get; set; }

        public void OnGet(string id)
        {
            Author = unit.Users.Get(id);
            Posts = unit.Posts.Find(p => p.AuthorId == Author.Id);

            var follower = usermanager.GetUserAsync(User).Result;

            var follow = new Follow
            {
                FollowerId = follower.Id,
                Follower = follower,
                FollowingId = Author.Id,
                Following = Author
            };

            IsFollowing = unit.Follows
                .Find(f => f.FollowerId == follow.FollowerId && f.FollowingId == follow.FollowingId)
                .FirstOrDefault() != null;
        }

        public IActionResult OnPostFollow(string followingId)
        {
            var follower = usermanager.GetUserAsync(User).Result;
            var following = usermanager.FindByIdAsync(followingId).Result;

            var follow = new Follow
            {
                FollowerId = follower.Id,
                Follower = follower,
                FollowingId = following.Id,
                Following = following
            };

            unit.Follows.Add(follow);
            unit.Complete();

            return RedirectToPage();
        }
        public IActionResult OnPostUnFollow(string followingId)
        {
            var follower = usermanager.GetUserAsync(User).Result;
            var following = usermanager.FindByIdAsync(followingId).Result;

            var follow = new Follow
            {
                FollowerId = follower.Id,
                Follower = follower,
                FollowingId = following.Id,
                Following = following
            };

            unit.Follows.Remove(follow);
            unit.Complete();

            return RedirectToPage();
        }
    }
}
