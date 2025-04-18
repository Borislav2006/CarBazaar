using Microsoft.AspNetCore.Identity;

namespace CarBazaar.Models
{
    public class User : IdentityUser    
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}
