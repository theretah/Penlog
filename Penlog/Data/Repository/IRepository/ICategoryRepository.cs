using Penlog.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        Category GetWithPicture(int id);
    }
}
