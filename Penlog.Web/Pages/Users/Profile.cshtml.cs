using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Configuration;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;
using System.Security.Claims;

namespace Penlog.Pages.Users
{
    public class ProfileModel : PageModel
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> usermanager;

        public ProfileModel(IUnitOfWork unit, UserManager<AppUser> usermanager)
        {
            this.unit = unit;
            this.usermanager = usermanager;
        }

        public IEnumerable<Post> Posts { get; set; }
        public AppUser Author { get; set; }
        public bool IsFollowing { get; set; }

        public void OnGet(string id)
        {
            Author = usermanager.FindByIdAsync(id).Result;
            Posts = unit.Posts.Find(p => p.AuthorId == Author.Id);

            var follow = GetFollowEntity(id);

            IsFollowing = unit.Follows
                .Find(f => f.FollowerId == follow.FollowerId && f.FollowingId == follow.FollowingId)
                .FirstOrDefault() != null;
        }
        public IActionResult OnPostFollow(string followingId)
        {
            unit.Follows.Add(GetFollowEntity(followingId));

            var follower = GetFollower().Result;
            follower.FollowingsCount++;
            var following = GetFollowing(followingId).Result;
            following.FollowersCount++;

            unit.Complete();

            return RedirectToPage();
        }
        public IActionResult OnPostUnFollow(string followingId)
        {
            unit.Follows.Remove(GetFollowEntity(followingId));

            var follower = GetFollower().Result;
            follower.FollowingsCount--;
            var following = GetFollowing(followingId).Result;
            following.FollowersCount--;

            unit.Complete();

            return RedirectToPage();
        }
        public IActionResult OnPostChangeProfilePhoto()
        {

            return RedirectToPage();
        }
        private Follow GetFollowEntity(string followingId)
        {
            var follower = GetFollower().Result;
            var following = GetFollowing(followingId).Result;

            if (follower == null)
                return new Follow();

            return new Follow
            {
                FollowerId = follower.Id,
                Follower = follower,
                FollowingId = following.Id,
                Following = following
            };
        }
        private async Task<AppUser> GetFollower()
        {
            // Follower is this user
            return await usermanager.GetUserAsync(User);
        }
        private async Task<AppUser> GetFollowing(string followingId)
        {
            return await usermanager.FindByIdAsync(followingId);
        }
    }
}
