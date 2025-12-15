using System.ComponentModel.DataAnnotations;

namespace KyrkPortalen.Domain.DTOs
{
    public class CreateCategoryDTO
    {
        [Required, MaxLength(80)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
