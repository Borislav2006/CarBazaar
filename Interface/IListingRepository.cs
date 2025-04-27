using CarBazaar.Helper;
using CarBazaar.Models;
using static CarBazaar.Repository.ListingRespository;

namespace CarBazaar.Interface
{
    public interface IListingRepository
    {
        Task<PaginatedResult<Listing>> GetAllAsync(QueryObject query);
        Task<Listing?> GetByIdAsync(int id);
        Task<List<Listing>> GetByUserIdAsync(string id, QueryObject query);
        Task<Listing> CreateListingAsync(Listing listing);
        Task<Listing> UpdateListingAsync(int id, Listing lisitngDto);
        Task<Listing> DeleteListingAsync(int id);
        Task<bool> SellerExist(int id);
    }
}
