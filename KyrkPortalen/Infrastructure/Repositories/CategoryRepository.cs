using KyrkPortalen.Domain.Entities;
using KyrkPortalen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KyrkPortalen.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        //  Hämta alla kategorier
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Activities)
                .ToListAsync();
        }

        // Hämta kategori via ID
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Activities)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Hämta kategori via namn (för auto-skapande)
        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        // Lägg till ny kategori
        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        // Uppdatera befintlig kategori
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await Task.CompletedTask;
        }

        // Ta bort kategori
        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await Task.CompletedTask;
        }

        // Spara ändringar
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
