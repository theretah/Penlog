namespace Penlog.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IUserRepository Users { get; }
        IFollowRepository Follows { get; }
        int Complete();
    }
}
