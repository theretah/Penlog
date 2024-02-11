using Microsoft.EntityFrameworkCore;
using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using Penlog.Entities;
using System.Linq.Expressions;

namespace Penlog.Data.Repository
{
    public class PostCategoryRepository : Repository<PostCategory>, IPostCategoryRepository
    {
        private readonly PenlogDbContext context;

        public PostCategoryRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(PostCategory postCategory)
        {
            context.Update(postCategory);
        }
        public IEnumerable<PostCategory> FindWithCategory(Expression<Func<PostCategory, bool>> predicate)
        {
            return context.PostCategories.Include(pc => pc.Category).Where(predicate).ToList();
        }
    }
}
