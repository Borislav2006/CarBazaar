using System.ComponentModel.DataAnnotations;

namespace CarBazaar.Dtos.Account
{
    public class RegisterDto
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
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email ")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
