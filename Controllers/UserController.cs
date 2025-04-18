//using System.Net;
//using CarBazaar.Dtos.User;
//using CarBazaar.Models;
//using CarBazaar.Interface;
//using CarBazaar.Mappers;
//using CarBazaar.Repository;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace CarBazaar.Controllers
//{
//    [ApiController]
//    [Route("api/user")]
//    public class UserController : ControllerBase
//    {
//        private readonly AppDbContext DBContext;
//        private readonly IUserRepository userRepository;
//        public UserController(AppDbContext DBContext, IUserRepository userRepository)
//        {
//            this.DBContext = DBContext;
//            this.userRepository = userRepository;
//        }


//        [HttpGet("{id:int}")]
//        public async Task<IActionResult> GetUserById([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var user = await userRepository.GetUserByIdAsync(id);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return Ok(user.ToUserDto());
//        }

//        [HttpPut("{id:int}")]
//        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequestDto updateDto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var user = await userRepository.UpdateUserAsync(id, updateDto.ToUserFromUpdateUserRequestDto());
//            if (user == null)
//            {
//                NotFound("User not found");
//            }

//            await DBContext.SaveChangesAsync();
//            return Ok(user.ToUserDto());
//        }
//    }
//}