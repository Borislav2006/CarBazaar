//using CarBazaar.Dtos.User;
//using CarBazaar.Models;
//using CarBazaar.Interface;
//using Microsoft.EntityFrameworkCore;

//namespace CarBazaar.Repository
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly AppDbContext DbContext;
//        public UserRepository(AppDbContext DbContext)
//        {
//            this.DbContext = DbContext;
//        }

//        public async Task<User?> GetUserByEmailAsync(string email)
//        {
//            return await DbContext.Users.FirstOrDefaultAsync(i => i.Email == email);
//        }

//        public async Task<User?> GetUserByIdAsync(int id)
//        {
//            return await DbContext.Users.Include(c => c.Listings).FirstOrDefaultAsync(i => i.Id == id);
//        }

//        public async Task<User> UpdateUserAsync(int id, UpdateUserRequestDto userDto)
//        {

//            var existingUser = await DbContext.Users.FindAsync(id);

//            if (existingUser == null)
//            {
//                return null;
//            }

//            existingUser.FirstName = userDto.FirstName;
//            existingUser.LastName = userDto.LastName;
//            existingUser.PhoneNumber = userDto.PhoneNumber;
//            existingUser.PasswordHash = userDto.Password;

//            await DbContext.SaveChangesAsync();
//            return existingUser;
//        }

//    }
//}
