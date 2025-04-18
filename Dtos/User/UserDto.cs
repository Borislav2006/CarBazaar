using CarBazaar.Dtos.Listing;

namespace CarBazaar.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<listingDto> Listings { get; set; }
    }

}
