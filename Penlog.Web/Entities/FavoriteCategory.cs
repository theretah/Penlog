namespace Penlog.Entities
{
    public class FavoriteCategory
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
