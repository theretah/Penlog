namespace Penlog.Model.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
