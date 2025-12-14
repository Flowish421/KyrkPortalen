using KyrkPortalen.Domain.Entities;

namespace KyrkPortalen.Infrastructure.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetAllAsync();
        Task<Activity?> GetByIdAsync(int id);
        Task<IEnumerable<Activity>> GetByUserAsync(int userId);
        Task AddAsync(Activity activity);
        Task UpdateAsync(Activity activity);
        Task DeleteAsync(Activity activity);
        Task SaveChangesAsync();
    }
}
