using Penlog.Model.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        void Update(Post post);
        IEnumerable<Post> GetAllWithAuthors();
        Post GetWithAuthor(int id);
    }
}
