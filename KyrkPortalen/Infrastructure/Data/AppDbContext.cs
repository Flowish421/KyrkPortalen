using KyrkPortalen.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace KyrkPortalen.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        public DbSet<Activity> Activities => Set<Activity>();
        public DbSet<Category> Categories => Set<Category>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation: User → Activities (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Activities)
                .WithOne(a => a.User!)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Ny relation: Category → Activities (1:N)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Activities)
                .WithOne(a => a.Category!)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
