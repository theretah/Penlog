using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

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
    }
}
