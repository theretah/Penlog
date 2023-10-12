using Penlog.Model.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        void Update(Comment comment);
    }
}
