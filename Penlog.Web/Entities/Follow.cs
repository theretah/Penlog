using Penlog.Entities;

namespace Penlog.Entities
{
    public class Follow
    {
        public string FollowerId { get; set; }
        public AppUser Follower { get; set; }

        public string FollowingId { get; set; }
        public AppUser Following { get; set; }
    }
}
