using CarBazaar.Dtos.User;
using CarBazaar.Models;

namespace CarBazaar.Dtos.Listing
{
    public class listingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Milage { get; set; }
        public decimal Price { get; set; }
        public string EngineType { get; set; }
        public int HorsePower { get; set; }
        public string GearBox { get; set; }
        public string Color { get; set; }
        public List<ListingImageDto> Images { get; set; } = new List<ListingImageDto>();
        public UserSummaryDto User { get; set; }
    }
}
