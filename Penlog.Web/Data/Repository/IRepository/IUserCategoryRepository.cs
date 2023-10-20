using Penlog.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface IUserCategoryRepository : IRepository<UserCategory>
    {
        void Update(UserCategory userCategory);
    }
}
