namespace Penlog.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IFollowRepository Follows { get; }
        IPhotoRepository Photos { get; }
        int Complete();
    }
}
