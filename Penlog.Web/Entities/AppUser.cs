using Microsoft.AspNetCore.Identity;

namespace Penlog.Model.Entities
{
    public class AppUser : IdentityUser
    {
        public IEnumerable<Post> Posts { get; set; }
        public int PostsCount { get; set; }
        public string? Biography { get; set; }

        public int FollowersCount { get; set; }
        public int FollowingsCount { get; set; }

        public IEnumerable<AppUser> Followers { get; set; }
        public IEnumerable<AppUser> Followings { get; set; }
    }
}
