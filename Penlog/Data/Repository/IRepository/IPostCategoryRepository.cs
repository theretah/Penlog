using Penlog.Entities;
using System.Linq.Expressions;

namespace Penlog.Data.Repository.IRepository
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    {
        void Update(PostCategory postCategory);
        public IEnumerable<PostCategory> FindWithCategory(Expression<Func<PostCategory, bool>> predicate);
    }
}
