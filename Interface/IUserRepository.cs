using CarBazaar.Dtos.User;
using CarBazaar.Models;

namespace CarBazaar.Interface
{
    public interface IUserRepository
    {
        //Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        //Task<User> UpdateUserAsync(int id, UpdateUserRequestDto userDto);
    }
}
