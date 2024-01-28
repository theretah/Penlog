﻿namespace Penlog.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IFollowRepository Follows { get; }
        IImageRepository Images { get; }
        ILikeRepository Likes { get; }
        ICommentRepository Comments { get; }
        IUserCategoryRepository UserCategories { get; }
        IPostCategoryRepository PostCategories { get; }
        ICategoryRepository Categories { get; }
        IFavoriteCategoryRepository FavoriteCategories { get; }
        int Complete();
    }
}
