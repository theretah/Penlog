using Penlog.Model.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface ILikeRepository : IRepository<Like>
    {
        void Update(Like like);
    }
}
