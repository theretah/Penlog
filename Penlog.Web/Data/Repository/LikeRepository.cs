using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;

namespace Penlog.Data.Repository
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly PenlogDbContext context;

        public LikeRepository(PenlogDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Like(int postId, string userId)
        {
            context.Likes.Add(new Like { PostId = postId, UserId = userId });
        }

        public void UnLike(int postId, string userId)
        {
            context.Likes.Remove(context.Likes.SingleOrDefault(l => l.PostId == postId && l.UserId == userId));
        }

        public void Update(Like like)
        {
            context.Update(like);
        }
    }
}
