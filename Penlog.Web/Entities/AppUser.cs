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

        public IEnumerable<Follow> Follows { get; set; }
    }
    public class Follow
    {
        public string FollowerId { get; set; }
        public AppUser Follower { get; set; }

        public string FollowingId { get; set; }
        public AppUser Following { get; set; }
    }
}
