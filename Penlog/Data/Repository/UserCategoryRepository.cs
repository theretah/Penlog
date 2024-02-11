using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Data.Repository
{
    public class UserCategoryRepository : Repository<UserCategory>, IUserCategoryRepository
    {
        private readonly PenlogDbContext context;

        public UserCategoryRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(UserCategory userCategory)
        {
            context.Update(userCategory);
        }
    }
}
