using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KyrkPortalen.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
