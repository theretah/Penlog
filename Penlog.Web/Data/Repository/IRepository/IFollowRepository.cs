using Microsoft.AspNetCore.Mvc;
using Penlog.Model.Entities;

namespace Penlog.Data.Repository.IRepository
{
    public interface IFollowRepository : IRepository<Follow>
    {
        void Update(Follow follow);
        void Follow(string followerId, string followingId);
        void UnFollow(string followerId, string followingId);
    }
}
