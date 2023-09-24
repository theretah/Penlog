using Penlog.Model.Entities;

namespace Penlog.PageModels
{
    public interface IFollowPageControls
    {
        Follow GetFollowEntity(string followerId, string followingId);
        void Follow(string followerId, string followingId);
        void UnFollow(string followerId, string followingId);
    }
}