using CarBazaar.Dtos.Account;
using CarBazaar.Interface;
using CarBazaar.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarBazaar.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User>  userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenService tokenService;    
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if(user == null) return Unauthorized("Invalid Email!");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Email not found and/or password incorrect");

            return Ok(
                new NewUserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Token = tokenService.CreateToken(user)
                });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto resigterDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new User
                {
                    FirstName = resigterDto.FirstName,
                    LastName = resigterDto.LastName,
                    UserName = resigterDto.FirstName + resigterDto.LastName,
                    Email = resigterDto.Email,
                    PhoneNumber = resigterDto.PhoneNumber,
                };

              var createdUser = await userManager.CreateAsync(user, resigterDto.Password);
                if (createdUser.Succeeded)
                {
                    return Ok(new NewUserDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Token = tokenService.CreateToken(user)
                    });
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
                
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
