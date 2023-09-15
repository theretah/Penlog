using Microsoft.AspNetCore.Identity;

namespace Penlog.Model.Entities
{
    public class AppUser : IdentityUser
    {
        public IEnumerable<Post> Posts { get; set; }
        public int PostsCount { get; set; }
        public string? Biography { get; set; }
    }

}
