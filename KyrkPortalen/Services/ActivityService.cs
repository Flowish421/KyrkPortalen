using KyrkPortalen.Domain.DTOs;
using KyrkPortalen.Domain.Entities;
using KyrkPortalen.Domain.Enums;
using KyrkPortalen.Infrastructure.Repositories;

namespace KyrkPortalen.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repo;

        public ActivityService(IActivityRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ActivityDTO>> GetAllAsync()
        {
            var activities = await _repo.GetAllAsync();
            return activities.Select(a => new ActivityDTO
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                Category = a.Category.ToString(),
                CreatedBy = a.User?.FullName ?? "Okänd"
            });
        }

        public async Task<ActivityDTO?> GetByIdAsync(int id)
        {
            var a = await _repo.GetByIdAsync(id);
            if (a == null) return null;
            return new ActivityDTO
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                Category = a.Category.ToString(),
                CreatedBy = a.User?.FullName ?? "Okänd"
            };
        }

        public async Task<IEnumerable<ActivityDTO>> GetByUserAsync(int userId)
        {
            var activities = await _repo.GetByUserAsync(userId);
            return activities.Select(a => new ActivityDTO
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                Category = a.Category.ToString(),
                CreatedBy = a.User?.FullName ?? "Okänd"
            });
        }

        public async Task<ActivityDTO> CreateAsync(int userId, CreateActivityDTO dto)
        {
            Enum.TryParse(dto.Category, out ActivityCategory category);

            var activity = new Activity
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId,
                Category = category
            };

            await _repo.AddAsync(activity);
            await _repo.SaveChangesAsync();

            return new ActivityDTO
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                CreatedAt = activity.CreatedAt,
                Category = activity.Category.ToString(),
                CreatedBy = ""
            };
        }

        public async Task<bool> UpdateAsync(int id, int userId, UpdateActivityDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null || existing.UserId != userId) return false;

            Enum.TryParse(dto.Category, out ActivityCategory category);
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Category = category;

            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, int userId, bool isAdmin)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            if (!isAdmin && existing.UserId != userId) return false;

            await _repo.DeleteAsync(existing);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
