using KyrkPortalen.Domain.DTOs;

namespace KyrkPortalen.Services
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDTO>> GetAllAsync();
        Task<ActivityDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ActivityDTO>> GetByUserAsync(int userId);
        Task<ActivityDTO> CreateAsync(int userId, CreateActivityDTO dto);
        Task<ActivityDTO?> UpdateAsync(int id, int userId, UpdateActivityDTO dto, bool isAdmin = false); // ✅ denna!
        Task<bool> DeleteAsync(int id, int userId, bool isAdmin = false);
    }
}
