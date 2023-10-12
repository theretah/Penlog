using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

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
    }
}
