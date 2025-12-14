using System.ComponentModel.DataAnnotations;

namespace KyrkPortalen.Domain.DTOs
{
    public class UpdateActivityDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = "General";
    }
}
