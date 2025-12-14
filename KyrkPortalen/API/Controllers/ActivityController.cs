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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var activities = await _service.GetAllAsync();
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var activity = await _service.GetByIdAsync(id);
            if (activity == null) return NotFound();
            return Ok(activity);
        }

        // 👇 only logged-in users can create
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActivityDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // 👇 grab userId from JWT claim instead of route
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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID missing in token");

            int userId = int.Parse(userIdClaim);

            var updated = await _service.UpdateAsync(id, userId, dto);
            if (!updated) return Forbid();
            return NoContent();
        }

        // 👇 only admins or owners can delete
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] bool isAdmin = false)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID missing in token");

            int userId = int.Parse(userIdClaim);

            var deleted = await _service.DeleteAsync(id, userId, isAdmin);
            if (!deleted) return Forbid();
            return NoContent();
        }
    }
}
