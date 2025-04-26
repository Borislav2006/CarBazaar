using CarBazaar.Models;
using CarBazaar.Interface;
using Microsoft.EntityFrameworkCore;
using CarBazaar.Helper;

namespace CarBazaar.Repository
{
    public class ListingRespository : IListingRepository
    {
        private readonly AppDbContext DbContext;
        public ListingRespository(AppDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public async Task<Listing> CreateListingAsync(Listing listing)
        {
            await DbContext.Listings.AddAsync(listing);
            await DbContext.SaveChangesAsync();
            return listing;
        }

        public async Task<Listing> DeleteListingAsync(int id)
        {
            var listing = await DbContext.Listings.FirstOrDefaultAsync(x => x.Id == id); 

            if (listing == null)
            {
                return null;
            }

            DbContext.Listings.Remove(listing);
            await DbContext.SaveChangesAsync();
            
            return listing;
        }

        public async Task<List<Listing>> GetAllAsync(QueryObject query)
        {
            var listings = DbContext.Listings.Include(i => i.ListingImages).Include(s => s.Seller).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                listings = listings.Where(s => s.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.Model))
            {
                listings = listings.Where(s => s.Model == query.Model);
            }

            if (!string.IsNullOrWhiteSpace(query.EngineType))
            {
                listings = listings.Where(s => s.EngineType == query.EngineType);
            }

            if (!string.IsNullOrWhiteSpace(query.GearBox))
            {
                listings = listings.Where(s => s.GearBox == query.GearBox);
            }

            if (!string.IsNullOrWhiteSpace(query.Color))
            {
                listings = listings.Where(s => s.Color == query.Color);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Brand", StringComparison.OrdinalIgnoreCase))
                {
                    listings = query.IsDecsending ? listings.OrderByDescending(s => s.Brand) : listings.OrderBy(s => s.Brand);
                }
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Model", StringComparison.OrdinalIgnoreCase))
                {
                    listings = query.IsDecsending ? listings.OrderByDescending(s => s.Model) : listings.OrderBy(s => s.Model);
                }
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    listings = query.IsDecsending ? listings.OrderByDescending(s => s.Price) : listings.OrderBy(s => s.Price);
                }
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Created", StringComparison.OrdinalIgnoreCase))
                {
                    listings = query.IsDecsending ? listings.OrderByDescending(s => s.CreatedAt) : listings.OrderBy(s => s.CreatedAt);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await listings.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Listing?> GetByIdAsync(int id)
        {
           var listing = await DbContext.Listings.AsNoTracking().Include(l => l.ListingImages).Include(s => s.Seller).FirstOrDefaultAsync(i => i.Id == id);
            return listing;
        }

        public async Task<List<Listing>> GetByUserIdAsync(string userId)
        {
            var listings = await DbContext.Listings
                .AsNoTracking()
                .Include(l => l.ListingImages)
                .Include(l => l.Seller)
                .Where(l => l.SellerId == userId)
                .ToListAsync();

            return listings;
        }

        public Task<bool> SellerExist(int id)
        {
            return DbContext.Listings.AnyAsync(x => x.Id == id);
        }

        public async Task<Listing> UpdateListingAsync(int id, Listing lisitngDto)
        {
            var existingLisitng = await DbContext.Listings.FindAsync(id);

            if (existingLisitng == null)
            {
                return null;
            }

            existingLisitng.Title = lisitngDto.Title;
            existingLisitng.Description = lisitngDto.Description;
            existingLisitng.Brand = lisitngDto.Brand;
            existingLisitng.Model = lisitngDto.Model;
            existingLisitng.Year = lisitngDto.Year;
            existingLisitng.Milage = lisitngDto.Milage;
            existingLisitng.Price = lisitngDto.Price;
            existingLisitng.EngineType = lisitngDto.EngineType;
            existingLisitng.HorsePower = lisitngDto.HorsePower;
            existingLisitng.GearBox = lisitngDto.GearBox;
            existingLisitng.Color = lisitngDto.Color;
            existingLisitng.ListingImages = lisitngDto.ListingImages;
           

            await DbContext.SaveChangesAsync();

            return existingLisitng;
        }
    }
}
