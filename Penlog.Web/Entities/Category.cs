using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Penlog.Model.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Penlog.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int PictureId { get; set; }
        [ValidateNever]
        public Image Picture { get; set; }

        public int? ParentId { get; set; }
        [ValidateNever]
        public Category Parent { get; set; }
        [ValidateNever]
        public IEnumerable<Category> SubCategories { get; set; }

        [ValidateNever]
        public IEnumerable<PostCategory> Posts { get; set; }
        [ValidateNever]
        public IEnumerable<UserCategory> Followers { get; set; }
    }
}
