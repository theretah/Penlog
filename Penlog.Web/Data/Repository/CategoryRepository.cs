using Microsoft.EntityFrameworkCore;
using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

namespace Penlog.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly PenlogDbContext context;

        public CategoryRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Category category)
        {
            context.Update(category);
        }

        public Category GetWithPicture(int id)
        {
            return context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
