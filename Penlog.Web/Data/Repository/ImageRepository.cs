using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Data.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private readonly PenlogDbContext context;

        public ImageRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Image image)
        {
            context.Update(image);
        }
    }
}
