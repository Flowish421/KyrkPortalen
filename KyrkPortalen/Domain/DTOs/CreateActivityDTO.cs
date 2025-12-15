namespace KyrkPortalen.Domain.DTOs
{
    public class CreateActivityDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // 🔹 Gör CategoryId valfri
        public int? CategoryId { get; set; }
    }
}
