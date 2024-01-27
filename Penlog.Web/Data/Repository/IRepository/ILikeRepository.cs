using Penlog.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface ILikeRepository : IRepository<Like>
    {
        void Update(Like like);
        void Like(int postId, string userId);
        void UnLike(int postId, string userId);
    }
}
