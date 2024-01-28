using Penlog.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface IFavoriteCategoryRepository : IRepository<FavoriteCategory>
    {
        void Update(FavoriteCategory favoriteCategory);
    }
}
