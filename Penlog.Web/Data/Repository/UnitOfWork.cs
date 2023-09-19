using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;

namespace Penlog.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PenlogDbContext context;

        public UnitOfWork(PenlogDbContext context)
        {
            this.context = context;
            Posts = new PostRepository(context);
            Follows = new FollowRepository(context);
        }

        public IPostRepository Posts { get; private set; }
        public IFollowRepository Follows { get; private set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
