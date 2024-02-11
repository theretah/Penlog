using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Penlog.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Penlog.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [ValidateNever]
        public IEnumerable<PostCategory> Posts { get; set; }
        [ValidateNever]
        public IEnumerable<UserCategory> Followers { get; set; }
        [ValidateNever]
        public IEnumerable<FavoriteCategory> FavoriteFollowers { get; set; }
    }
}
