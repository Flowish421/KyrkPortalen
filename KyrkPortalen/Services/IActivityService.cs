using KyrkPortalen.Domain.DTOs;

namespace KyrkPortalen.Services
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDTO>> GetAllAsync();
        Task<ActivityDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ActivityDTO>> GetByUserAsync(int userId);
        Task<ActivityDTO> CreateAsync(int userId, CreateActivityDTO dto);
        Task<bool> UpdateAsync(int id, int userId, UpdateActivityDTO dto);
        Task<bool> DeleteAsync(int id, int userId, bool isAdmin);
    }
}
