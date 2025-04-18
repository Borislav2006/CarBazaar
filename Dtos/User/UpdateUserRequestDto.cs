using System.ComponentModel.DataAnnotations;

namespace CarBazaar.Dtos.User
{
    public class UpdateUserRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "First name should be atleast 3 characters")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Last name should be atleast 3 characters")]
        public string LastName { get; set; }
        [Required]
        [Phone(ErrorMessage = "Enter a valid phone number")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
