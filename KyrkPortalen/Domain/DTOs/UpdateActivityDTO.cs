using System.ComponentModel.DataAnnotations;

namespace KyrkPortalen.Domain.DTOs
{
    public class UpdateActivityDTO
    {
        public int? UserId { get; set; } 
        
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        
        public int? CategoryId { get; set; }   // ✅ nytt fält
    }
}
