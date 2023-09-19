using Penlog.Model.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        void Update(Photo photo);
    }
}
