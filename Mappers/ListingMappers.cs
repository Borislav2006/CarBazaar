using CarBazaar.Dtos.Listing;
using CarBazaar.Dtos.Listing.CarBazaar;
using CarBazaar.Dtos.User;
using CarBazaar.Models;

namespace CarBazaar.Mappers
{
    public static class ListingMappers
    {
        public static ListingDto ToListingDto(this Listing listing)
        {

            return new ListingDto
            {
                Id = listing.Id,
                Title = listing.Title,
                Description = listing.Description,
                Brand = listing.Brand,
                Model = listing.Model,
                Year = listing.Year,
                Milage = listing.Milage,
                Price = listing.Price,
                EngineType = listing.EngineType,
                FuelType = listing.FuelType,
                HorsePower = listing.HorsePower,
                GearBox = listing.GearBox,
                Color = listing.Color,
                CreatedAt = listing.CreatedAt,
                UpdatedAt = listing.UpdatedAt,
                Images = listing.ListingImages.Select(img => new ListingImageDto
                {
                    Id = img.Id,
                    ImagePath = img.ImgPath,
                }).ToList(),
                User = new UserSummaryDto
                {
                    FirstName = listing.Seller.FirstName,
                    LastName = listing.Seller.LastName,
                    Email = listing.Seller.Email,
                    PhoneNumber = listing.Seller.PhoneNumber,
                }
            };

        }
        public static async Task<Listing> ToListingFromCreateListing(this CreateListingRequestDto listingDto, string sellerId)
        {
            var listing =  new Listing
            {
                Title = listingDto.Title,
                Description = listingDto.Description,
                Brand = listingDto.Brand,
                Model = listingDto.Model,
                Year = listingDto.Year,
                Milage = listingDto.Milage,
                Price = listingDto.Price,
                EngineType = listingDto.EngineType,
                FuelType = listingDto.FuelType,
                HorsePower = listingDto.HorsePower,
                GearBox = listingDto.GearBox,
                Color = listingDto.Color,
                SellerId = sellerId,
                ListingImages = new List<ListingImage>()
            };

            if (listingDto.Images != null)
            {
                foreach (var image in listingDto.Images)
                {
                    var fileName = $"{Guid.NewGuid()}.jpg";

                    string baseDirectory = Environment.GetEnvironmentVariable("Image_Folder");

                    var filePath = Path.Combine(baseDirectory, fileName);

                    if (!Directory.Exists(baseDirectory))
                    {
                        Directory.CreateDirectory(baseDirectory);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    listing.ListingImages.Add(new ListingImage
                    {
                        ImgPath = fileName
                    });
                }
            }
            return listing;
        }
        public static async Task<Listing> ToListingFromUpdateListing(this UpdateListingRequestDto listingDto)
        {
            var listing = new Listing
            {
                Title = listingDto.Title,
                Description = listingDto.Description,
                Brand = listingDto.Brand,
                Model = listingDto.Model,
                Year = listingDto.Year,
                Milage = listingDto.Milage,
                Price = listingDto.Price,
                EngineType = listingDto.EngineType,
                FuelType = listingDto.FuelType,
                HorsePower = listingDto.HorsePower,
                GearBox = listingDto.GearBox,
                Color = listingDto.Color,
                ListingImages = new List<ListingImage>()
            };

            if (listingDto.Images != null)
            {
                foreach (var image in listingDto.Images)
                {
                    var fileName = $"{Guid.NewGuid()}.jpg";

                    string baseDirectory = Environment.GetEnvironmentVariable("Image_Folder");

                    var filePath = Path.Combine(baseDirectory, fileName);

                    if (!Directory.Exists(baseDirectory))
                    {
                        Directory.CreateDirectory(baseDirectory);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    listing.ListingImages.Add(new ListingImage
                    {
                        ImgPath = fileName
                    });
                }
            }

            return listing;
        }
    }
}
