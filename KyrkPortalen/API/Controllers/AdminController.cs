using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KyrkPortalen.Domain.DTOs;
using KyrkPortalen.Services;
using System.Security.Claims;
using KyrkPortalen.Infrastructure.Repositories;


namespace KyrkPortalen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IUserRepository _userRepository;

        public AdminController(IActivityService activityService, IUserRepository userRepository)
        {
            _activityService = activityService;
            _userRepository = userRepository;
        }

        // 🔹 Hämta alla aktiviteter
        [HttpGet("activities")]
        public async Task<IActionResult> GetAllActivities()
        {
            var activities = await _activityService.GetAllAsync();
            return Ok(activities);
        }

        // 🔹 Uppdatera aktivitet (Admin)
        [HttpPut("activities/{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] UpdateActivityDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID saknas i token.");

            int adminId = int.Parse(userIdClaim);

            var updated = await _activityService.UpdateAsync(id, adminId, dto, true);
            if (updated == null)
                return NotFound("Aktiviteten kunde inte uppdateras.");

            return Ok(updated);
        }

        // 🔹 Ta bort aktivitet (Admin)
        [HttpDelete("activities/{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID saknas i token.");

            int adminId = int.Parse(userIdClaim);

            var deleted = await _activityService.DeleteAsync(id, adminId, true);
            if (!deleted)
                return NotFound("Kunde inte radera aktivitet.");

            return NoContent();
        }

        // 🔹 Visa alla registrerade användare
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users.Select(u => new
            {
                u.Id,
                u.FullName,
                u.Email,
                u.Role
            }));
        }
    }
}
