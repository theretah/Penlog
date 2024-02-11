using Microsoft.EntityFrameworkCore;
using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;
using System.Linq.Expressions;

namespace Penlog.Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly PenlogDbContext context;

        public CommentRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Comment comment)
        {
            context.Update(comment);
        }
        public IEnumerable<Comment> GetWithAuthors(Expression<Func<Comment, bool>>? predicate)
        {
            return context.Comments.Where(predicate).Include(c => c.Author).ToList();
        }
    }
}
