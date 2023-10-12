﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;
using System.Security.Claims;

namespace Penlog.PageModels
{
    public class FollowPageControls : IFollowPageControls
    {
        private readonly UserManager<AppUser> usermanager;
        private readonly IUnitOfWork unit;

        public FollowPageControls(UserManager<AppUser> usermanager, IUnitOfWork unit)
        {
            this.usermanager = usermanager;
            this.unit = unit;
        }

        public void Follow(string followerId, string followingId)
        {
            unit.Follows.Add(new Follow { FollowerId = followerId, FollowingId = followingId });

            var follower = GetFollower(followerId).Result;
            follower.FollowingsCount++;
            var following = GetFollowing(followingId).Result;
            following.FollowersCount++;

            unit.Complete();
        }
        public void UnFollow(string followerId, string followingId)
        {
            unit.Follows.Remove(new Follow { FollowerId = followerId, FollowingId = followingId });

            var follower = GetFollower(followerId).Result;
            follower.FollowingsCount--;
            var following = GetFollowing(followingId).Result;
            following.FollowersCount--;

            unit.Complete();
        }

        //private Follow GetFollowEntity(string followerId, string followingId)
        //{
        //    var follower = GetFollower(followerId).Result;
        //    var following = GetFollowing(followingId).Result;

        //    if (follower == null)
        //        return new Follow();

        //    return new Follow
        //    {
        //        FollowerId = followerId,
        //        Follower = follower,
        //        FollowingId = followingId,
        //        Following = following
        //    };
        //}
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
