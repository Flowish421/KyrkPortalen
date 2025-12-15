using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KyrkPortalen.Domain.DTOs;
using KyrkPortalen.Services;

namespace KyrkPortalen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _service;

        public ActivityController(IActivityService service)
        {
            _service = service;
        }

        // Om admin är inloggad → ser alla aktiviteter
        // Om vanlig användare → ser bara sina egna
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID missing in token");

            int userId = int.Parse(userIdClaim);
            bool isAdmin = User.IsInRole("Admin");

            if (isAdmin)
            {
                var all = await _service.GetAllAsync();
                return Ok(all);
            }

            var mine = await _service.GetByUserAsync(userId);
            return Ok(mine);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var activity = await _service.GetByIdAsync(id);
            if (activity == null) return NotFound();
            return Ok(activity);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActivityDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID missing in token");

            int userId = int.Parse(userIdClaim);

            var created = await _service.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateActivityDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID missing in token");

            int userId = int.Parse(userIdClaim);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            bool isAdmin = role == "Admin";

            var updated = await _service.UpdateAsync(id, userId, dto, isAdmin);
            if (updated == null)
                return Forbid();

            return Ok(updated);
        }

        // User får ta bort sin egen inlägg, Admin får ta bort alla
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID missing in token");

            int userId = int.Parse(userIdClaim);
            bool isAdmin = User.IsInRole("Admin");

            var deleted = await _service.DeleteAsync(id, userId, isAdmin);
            if (!deleted)
                return Forbid(); //om användare försöker ta bort någon annans

            return NoContent();
        }
    }
}
