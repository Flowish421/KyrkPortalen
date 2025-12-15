using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using KyrkPortalen.Services;
using KyrkPortalen.Domain.DTOs;
using KyrkPortalen.Domain.Entities;
using KyrkPortalen.Infrastructure.Repositories;

namespace KyrkPortalen.Tests
{
    public class ActivityServiceTests
    {
        private readonly Mock<IActivityRepository> _mockActivityRepo;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly ActivityService _service;

        public ActivityServiceTests()
        {
            _mockActivityRepo = new Mock<IActivityRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _service = new ActivityService(_mockActivityRepo.Object, _mockCategoryRepo.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnActivityDTO_WhenValidInput()
        {
            var dto = new CreateActivityDTO { Title = "Gudstjänst Söndag", Description = "Morgonmässa" };

            _mockCategoryRepo.Setup(c => c.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Category?)null);
            _mockCategoryRepo.Setup(c => c.AddAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);
            _mockCategoryRepo.Setup(c => c.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockActivityRepo.Setup(r => r.AddAsync(It.IsAny<Activity>())).Returns(Task.CompletedTask);
            _mockActivityRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _service.CreateAsync(1, dto);

            Assert.NotNull(result);
            Assert.Contains("Gudstjänst", result.Title);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoActivities()
        {
            _mockActivityRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Activity>());
            var result = await _service.GetAllAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnList_WhenActivitiesExist()
        {
            _mockActivityRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Activity>
            {
                new Activity { Id = 1, Title = "Resa till Rom", Description = "Pilgrimsresa" }
            });

            var result = await _service.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockActivityRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Activity?)null);
            var result = await _service.GetByIdAsync(99);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnActivity_WhenFound()
        {
            var activity = new Activity { Id = 1, Title = "Läger", Description = "Ungdomsläger" };
            _mockActivityRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(activity);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Läger", result?.Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenNotOwnerAndNotAdmin()
        {
            var activity = new Activity { Id = 1, Title = "Gamla", UserId = 1 };
            _mockActivityRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(activity);

            var dto = new UpdateActivityDTO { Title = "Ny titel" };
            var result = await _service.UpdateAsync(1, 2, dto, false);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNotFound()
        {
            _mockActivityRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Activity?)null);
            var result = await _service.DeleteAsync(1, 1, false);
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenAdminDeletes()
        {
            var activity = new Activity { Id = 1, UserId = 1, Title = "Test" };
            _mockActivityRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(activity);
            _mockActivityRepo.Setup(r => r.DeleteAsync(activity)).Returns(Task.CompletedTask);
            _mockActivityRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _service.DeleteAsync(1, 99, true);
            Assert.True(result);
        }
    }
}
