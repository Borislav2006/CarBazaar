using System.Net;
using CarBazaar.Dtos.Listing;
using CarBazaar.Dtos.Listing.CarBazaar;
using CarBazaar.Dtos.User;
using CarBazaar.Models;
using CarBazaar.Interface;
using CarBazaar.Mappers;
using CarBazaar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using CarBazaar.Helper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace CarBazaar.Controllers
{
    [ApiController]
    [Route("api/listing")]
    public class ListingsController : Controller
    {
        private readonly IListingRepository listingRepository;
        private readonly IUserRepository userRepository;

        public ListingsController(IListingRepository listingRepository, IUserRepository userRepository)
        {
            this.listingRepository = listingRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var listing = await listingRepository.GetAllAsync(query);

            var listingDto = listing.Items.Select(s => s.ToListingDto()).ToList();

            var response = new
            {
                Items = listingDto,
                TotalCount = listing.TotalCount,
                TotalPages = Math.Ceiling((double)listing.TotalCount / query.PageSize)
            };

            return Ok(response);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserListings([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var user = await userRepository.GetUserByEmailAsync(userEmail);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            var listing = await listingRepository.GetByUserIdAsync(user.Id, query);

            var listingDto = listing.Select(s => s.ToListingDto()).ToList();

            return Ok(listingDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetListingById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var listing = await listingRepository.GetByIdAsync(id);

            if (listing == null)
            {
                return NotFound();
            }
            return Ok(listing.ToListingDto());
        }


        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> CreateListing([FromForm] CreateListingRequestDto ListingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sellerEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (sellerEmail == null)
                return Unauthorized("You are not authorized.");

            var user = await userRepository.GetUserByEmailAsync(sellerEmail);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            var listing = await ListingDto.ToListingFromCreateListing(user.Id);
            await listingRepository.CreateListingAsync(listing);

            return CreatedAtAction(nameof(GetListingById), new { id = listing.Id }, listing.ToListingDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateLisitng([FromRoute] int id, [FromForm] UpdateListingRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updaterEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (updaterEmail == null)
            {
                return Unauthorized("You are not authorized.");
            }

            var user = await userRepository.GetUserByEmailAsync(updaterEmail);

            var listing = await listingRepository.GetByIdAsync(id);

            if (user.Id != listing.SellerId)
            {
                return Unauthorized("You are not the owner!");
            }

            var updatedListing = await updateDto.ToListingFromUpdateListing();

           await listingRepository.UpdateListingAsync(id, updatedListing);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteListing([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updaterEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (updaterEmail == null)
            {
                return Unauthorized("You are not authorized.");
            }

            var user = await userRepository.GetUserByEmailAsync(updaterEmail);

            var listing = await listingRepository.GetByIdAsync(id);

            if (user.Id != listing?.SellerId)
            {
                return Unauthorized("You are not the owner!");
            }

            var deletedListing = await listingRepository.DeleteListingAsync(id);

            if (deletedListing == null)
            {
                return NotFound("Lisitng does not exist");
            }
            return Ok(deletedListing);
        }
    }
}
