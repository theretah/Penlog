using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.PageModels
{
    public interface IFollowPageControls
    {
        void Follow(string followerId, string followingId);
        void UnFollow(string followerId, string followingId);
    }
}