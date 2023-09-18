using Penlog.Model.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface IFollowRepository : IRepository<Follow>
    {
        void Update(Follow follow);
    }
}
