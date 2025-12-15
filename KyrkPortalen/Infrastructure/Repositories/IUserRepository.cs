using KyrkPortalen.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KyrkPortalen.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
