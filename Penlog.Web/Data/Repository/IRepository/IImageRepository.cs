using Penlog.Model.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface IImageRepository : IRepository<Image>
    {
        void Update(Image image);
    }
}
