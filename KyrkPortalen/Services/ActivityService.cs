using KyrkPortalen.Domain.DTOs;
using KyrkPortalen.Domain.Entities;
using KyrkPortalen.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KyrkPortalen.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repo;
        private readonly ICategoryRepository _categoryRepo;

        public ActivityService(IActivityRepository repo, ICategoryRepository categoryRepo)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
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
                Category = a.Category?.Name ?? "Okänd",
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
                Category = a.Category?.Name ?? "Okänd",
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
                Category = a.Category?.Name ?? "Okänd",
                CreatedBy = a.User?.FullName ?? "Okänd"
            });
        }

        //Skapa aktivitet med automatisk kategori
        public async Task<ActivityDTO> CreateAsync(int userId, CreateActivityDTO dto)
        {
            var firstWord = dto.Title.Split(' ', System.StringSplitOptions.RemoveEmptyEntries)
                                     .FirstOrDefault()?.Trim();

            Category? category = null;

            if (!string.IsNullOrEmpty(firstWord))
            {
                category = await _categoryRepo.GetByNameAsync(firstWord);

                if (category == null)
                {
                    category = new Category
                    {
                        Name = firstWord,
                        Description = $"Automatiskt skapad kategori för '{firstWord}'"
                    };

                    await _categoryRepo.AddAsync(category);
                    await _categoryRepo.SaveChangesAsync();
                }
            }

            var activity = new Activity
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId,
                CategoryId = category?.Id
            };

            await _repo.AddAsync(activity);
            await _repo.SaveChangesAsync();

            return new ActivityDTO
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                CreatedAt = activity.CreatedAt,
                Category = category?.Name ?? "Ingen kategori",
                CreatedBy = ""
            };
        }

        //Uppdatera aktivitet (User = egna, Admin = alla)
        public async Task<ActivityDTO?> UpdateAsync(int id, int userId, UpdateActivityDTO dto, bool isAdmin = false)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return null;

            //Vanlig användare får bara redigera sina egna aktiviteter
            if (!isAdmin && existing.UserId != userId)
                return null;

            existing.Title = dto.Title ?? existing.Title;
            existing.Description = dto.Description ?? existing.Description;

            //Automatisk kategori baserad på titel
            var firstWord = dto.Title?.Split(' ', System.StringSplitOptions.RemoveEmptyEntries)
                                      .FirstOrDefault()?.Trim();

            if (!string.IsNullOrEmpty(firstWord))
            {
                var category = await _categoryRepo.GetByNameAsync(firstWord);
                if (category == null)
                {
                    category = new Category
                    {
                        Name = firstWord,
                        Description = $"Automatiskt skapad kategori för '{firstWord}'"
                    };
                    await _categoryRepo.AddAsync(category);
                    await _categoryRepo.SaveChangesAsync();
                }

                existing.CategoryId = category.Id;
            }

            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();

            var categoryEntity = existing.CategoryId.HasValue
                ? await _categoryRepo.GetByIdAsync(existing.CategoryId.Value)
                : null;

            return new ActivityDTO
            {
                Id = existing.Id,
                Title = existing.Title,
                Description = existing.Description,
                CreatedAt = existing.CreatedAt,
                Category = categoryEntity?.Name ?? "Okänd",
                CreatedBy = ""
            };
        }

        // Radera aktivitet (Admin = alla, User = egna)
        public async Task<bool> DeleteAsync(int id, int userId, bool isAdmin)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            if (!isAdmin && existing.UserId != userId)
                return false;

            await _repo.DeleteAsync(existing);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
