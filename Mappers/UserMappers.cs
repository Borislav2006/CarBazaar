//using CarBazaar.Dtos.User;
//using CarBazaar.Models;

//namespace CarBazaar.Mappers
//{
//    public static class UserMappers
//    {
//        public static UserDto ToUserDto(this User user)
//        {
//            return new UserDto
//            {
//                //Id = user.Id,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                Email = user.Email,
//                PhoneNumber = user.PhoneNumber,
//                Listings = user.Listings.Select(s => s.ToListingDto()).ToList(),
//            };
//        }

//        public static User ToUserFromUpdateUserRequestDto(this UpdateUserRequestDto userDto)
//        {
//            return new User
//            {
//                FirstName = userDto.FirstName,
//                LastName = userDto.LastName,
//                PhoneNumber = userDto.PhoneNumber,
//                PasswordHash = userDto.Password,
//            };
//        }
//    }
//}
