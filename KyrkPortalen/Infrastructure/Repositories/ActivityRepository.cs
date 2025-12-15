using KyrkPortalen.Domain.Entities;
using KyrkPortalen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KyrkPortalen.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            // ✅ Hämtar även Category och User (så vi slipper "Okänd")
            return await _context.Activities
                .Include(a => a.Category)
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Activity?> GetByIdAsync(int id)
        {
            return await _context.Activities
                .Include(a => a.Category)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Activity>> GetByUserAsync(int userId)
        {
            return await _context.Activities
                .Include(a => a.Category)
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Activity activity)
        {
            await _context.Activities.AddAsync(activity);
        }

        public async Task UpdateAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Activity activity)
        {
            _context.Activities.Remove(activity);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
