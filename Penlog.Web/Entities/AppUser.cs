using Microsoft.AspNetCore.Identity;
using Penlog.Entities;

namespace Penlog.Entities
{
    public class AppUser : IdentityUser
    {
        public int PostsCount { get; set; }
        public string? Biography { get; set; }

        public int FollowersCount { get; set; }
        public int FollowingsCount { get; set; }

        public int? ProfileImageId { get; set; }
        public Image? ProfileImage { get; set; }

        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Follow> Follows { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<UserCategory> UserCategories { get; set; }
        public IEnumerable<FavoriteCategory> FavoriteCategories { get; set; }
    }
}
