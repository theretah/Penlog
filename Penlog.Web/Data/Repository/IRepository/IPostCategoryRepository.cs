using Penlog.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    {
        void Update(PostCategory postCategory);
    }
}
