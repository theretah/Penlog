using Penlog.Entities;
using System.Linq.Expressions;

namespace Penlog.Data.Repository.IRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        void Update(Comment comment);
        IEnumerable<Comment> GetWithAuthors(Expression<Func<Comment, bool>>? predicate);
    }
}
