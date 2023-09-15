using Penlog.Model.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<AppUser>
    {
        void Update(AppUser author);
    }
}
