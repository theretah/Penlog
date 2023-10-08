using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Data.Repository
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly PenlogDbContext context;

        public LikeRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Like like)
        {
            context.Update(like);
        }
    }
}
