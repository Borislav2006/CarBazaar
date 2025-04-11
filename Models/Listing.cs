namespace CarBazaar.Models
{
    public class Listing
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int SellerId { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public int Milage { get; set; }

        public string EngineType { get; set; } = null!;

        public int HorsePower { get; set; }

        public string GearBox { get; set; } = null!;

        public string Color { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ListingImage> ListingImages { get; set; } = new List<ListingImage>();

        public virtual User Seller { get; set; } = null!;
    }
}
