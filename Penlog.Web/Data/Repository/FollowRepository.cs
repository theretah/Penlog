using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;
using System.Linq.Expressions;

namespace Penlog.Data.Repository
{
    public class FollowRepository : Repository<Follow>, IFollowRepository
    {
        private readonly PenlogDbContext context;

        public FollowRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Follow follow)
        {
            context.Update(follow);
        }
    }
}
