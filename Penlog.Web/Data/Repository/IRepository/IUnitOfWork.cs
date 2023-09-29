namespace Penlog.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IFollowRepository Follows { get; }
        IImageRepository Images { get; }
        int Complete();
    }
}
