using System.Collections.Generic;
using System.Threading.Tasks;
using KyrkPortalen.Domain.Entities;

namespace KyrkPortalen.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<bool> DeleteAsync(int id);
    }
}
