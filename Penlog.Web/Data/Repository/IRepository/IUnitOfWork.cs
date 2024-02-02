namespace Penlog.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IFollowRepository Follows { get; }
        IImageRepository Images { get; }
        ILikeRepository Likes { get; }
        ICommentRepository Comments { get; }
        IUserCategoryRepository UserCategories { get; }
        IFavoriteCategoryRepository FavoriteCategories { get; }
        IPostCategoryRepository PostCategories { get; }
        ICategoryRepository Categories { get; }
        int Complete();
    }
}
