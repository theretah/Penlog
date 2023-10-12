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
            Images = new ImageRepository(context);
            Likes = new LikeRepository(context);
            Comments = new CommentRepository(context);
        }

        public IPostRepository Posts { get; private set; }
        public IFollowRepository Follows { get; private set; }
        public IImageRepository Images { get; private set; }
        public ILikeRepository Likes { get; private set; }
        public ICommentRepository Comments { get; private set; }

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
