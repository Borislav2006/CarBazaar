namespace CarBazaar.Models
{
    public class ListingImage
    {
        public int Id { get; set; }

        public byte[]? Img { get; set; }

        public int ListingId { get; set; }

        public virtual Listing Listing { get; set; } = null!;
    }
}
