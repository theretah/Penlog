using Microsoft.AspNetCore.Identity;
using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PenlogDbContext context;

        public UnitOfWork(PenlogDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            Posts = new PostRepository(context);
            Follows = new FollowRepository(context, userManager);
            Images = new ImageRepository(context);
            Likes = new LikeRepository(context);
            Comments = new CommentRepository(context);
            PostCategories = new PostCategoryRepository(context);
            UserCategories = new UserCategoryRepository(context);
            Categories = new CategoryRepository(context);
        }

        public IPostRepository Posts { get; private set; }
        public IFollowRepository Follows { get; private set; }
        public IImageRepository Images { get; private set; }
        public ILikeRepository Likes { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IUserCategoryRepository UserCategories { get; private set; }
        public IPostCategoryRepository PostCategories { get; private set; }
        public ICategoryRepository Categories { get; private set; }

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
