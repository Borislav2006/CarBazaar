namespace CarBazaar.Models
{
    public class ListingImage
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int ListingId { get; set; }
        public virtual Listing Listing { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
