using Microsoft.EntityFrameworkCore;
using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly PenlogDbContext context;

        public PostRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Post> GetAllWithAuthors()
        {
            return context.Posts.Include(p => p.Author).ThenInclude(p => p.ProfileImage).ToList();
        }

        public Post GetWithAuthor(int id)
        {
            return context.Posts.Include(p => p.Author).ThenInclude(p => p.ProfileImage).Where(p => p.Id == id).FirstOrDefault();
        }

        public Post GetWithPreviewImage(int id)
        {
            return context.Posts.Include(p => p.Author).Include(p => p.PreviewImage).Where(p => p.Id == id).First();
        }

        public void Update(Post post)
        {
            context.Update(post);
        }
    }
}
