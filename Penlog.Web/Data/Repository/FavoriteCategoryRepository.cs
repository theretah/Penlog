using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Data.Repository
{
    public class FavoriteCategoryRepository : Repository<FavoriteCategory>, IFavoriteCategoryRepository
    {
        private readonly PenlogDbContext context;

        public FavoriteCategoryRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(FavoriteCategory favoriteCategory)
        {
            context.Update(favoriteCategory);
        }
    }
}
