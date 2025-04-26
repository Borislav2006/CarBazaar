using System.ComponentModel.DataAnnotations;

namespace CarBazaar.Dtos.Listing
{
    public class UpdateListingRequestDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Brand name should be atleast 3 characters")]
        public string Brand { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Model name should be atleast 3 characters")]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        [Range(1,100000)]
        public int Milage { get; set; }
        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }
        [Required]
        public string EngineType { get; set; }
        [Required]
        [Range(1,1000)]
        public int HorsePower { get; set; }
        [Required]
        public string GearBox { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Color name should be atleast 3 characters")]
        public string Color { get; set; }

        public List<IFormFile>? Images { get; set; }
    }
}
