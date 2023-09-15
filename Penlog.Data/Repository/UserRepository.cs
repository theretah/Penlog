using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Data.Repository
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private readonly PenlogDbContext context;

        public UserRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }
        public void Update(AppUser user)
        {
            context.Update(user);
        }
    }
}
