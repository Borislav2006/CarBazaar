using CarBazaar.Dtos.Listing;
using CarBazaar.Helper;
using CarBazaar.Models;

namespace CarBazaar.Interface
{
    public interface IListingRepository
    {
        Task<List<Listing>> GetAllAsync(QueryObject query);
        Task<Listing?> GetByIdAsync(int id);
        Task<Listing> CreateListingAsync(Listing listing);
        Task<Listing> UpdateListingAsync(int id, Listing lisitngDto);
        Task<Listing> DeleteListingAsync(int id);
        Task<bool> SellerExist(int id);
    }
}
