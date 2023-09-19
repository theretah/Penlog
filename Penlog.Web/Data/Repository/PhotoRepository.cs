using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Data.Repository
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private readonly PenlogDbContext context;

        public PhotoRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Photo photo)
        {
            context.Update(photo);
        }
    }
}
