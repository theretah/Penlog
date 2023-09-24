using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;
using System.Security.Claims;

namespace Penlog.PageModels
{
    public class FollowPageControls : IFollowPageControls
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> usermanager;

        public FollowPageControls(IUnitOfWork unit, UserManager<AppUser> usermanager)
        {
            this.unit = unit;
            this.usermanager = usermanager;
        }

        public void Follow(string followerId, string followingId)
        {
            unit.Follows.Add(GetFollowEntity(followerId, followingId));

            var follower = GetFollower(followerId).Result;
            follower.FollowingsCount++;
            var following = GetFollowing(followingId).Result;
            following.FollowersCount++;

            unit.Complete();
        }
        public void UnFollow(string followerId, string followingId)
        {
            unit.Follows.Remove(GetFollowEntity(followerId, followingId));

            var follower = GetFollower(followerId).Result;
            follower.FollowingsCount--;
            var following = GetFollowing(followingId).Result;
            following.FollowersCount--;

            unit.Complete();
        }

        public Follow GetFollowEntity(string followerId, string followingId)
        {
            var follower = GetFollower(followerId).Result;
            var following = GetFollowing(followingId).Result;

            if (follower == null)
                return new Follow();

            return new Follow
            {
                FollowerId = followerId,
                Follower = follower,
                FollowingId = followingId,
                Following = following
            };
        }
        private async Task<AppUser> GetFollower(string followerId)
        {
            // Follower must be the logged in user
            return await usermanager.FindByIdAsync(followerId);
        }
        private async Task<AppUser> GetFollowing(string followingId)
        {
            return await usermanager.FindByIdAsync(followingId);
        }
    }
}
