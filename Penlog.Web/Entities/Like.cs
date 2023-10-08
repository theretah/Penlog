using System.ComponentModel.DataAnnotations.Schema;

namespace Penlog.Model.Entities
{
    public class Like
    {
        public AppUser User { get; set; }
        public string UserId { get; set; }

        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
