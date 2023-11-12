using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Penlog.Data.Context;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;
using System.Linq.Expressions;

namespace Penlog.Data.Repository
{
    public class FollowRepository : Repository<Follow>, IFollowRepository
    {
        private readonly PenlogDbContext context;
        private readonly UserManager<AppUser> usermanager;

        public FollowRepository(PenlogDbContext context,UserManager<AppUser> usermanager) : base(context)
        {
            this.context = context;
            this.usermanager = usermanager;
        }

        public void Follow(string followerId, string followingId)
        {
            context.Follows.Add(new Follow { FollowerId = followerId, FollowingId = followingId });

            var follower = GetFollower(followerId).Result;
            follower.FollowingsCount++;
            var following = GetFollowing(followingId).Result;
            following.FollowersCount++;
        }

        public void UnFollow(string followerId, string followingId)
        {
            context.Follows.Remove(new Follow { FollowerId = followerId, FollowingId = followingId });

            var follower = GetFollower(followerId).Result;
            follower.FollowingsCount--;
            var following = GetFollowing(followingId).Result;
            following.FollowersCount--;
        }
        private async Task<AppUser> GetFollower(string followerId)
        {
            return await usermanager.FindByIdAsync(followerId);
        }
        private async Task<AppUser> GetFollowing(string followingId)
        {
            return await usermanager.FindByIdAsync(followingId);
        }

        public void Update(Follow follow)
        {
            context.Update(follow);
        }
    }
}
