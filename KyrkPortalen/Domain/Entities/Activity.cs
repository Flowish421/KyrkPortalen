using System.ComponentModel.DataAnnotations;
using KyrkPortalen.Domain.Enums;


namespace KyrkPortalen.Domain.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ActivityCategory Category { get; set; } = ActivityCategory.General;

        // Relationer
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
