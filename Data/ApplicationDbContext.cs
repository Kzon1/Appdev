using AppDev.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<Store> Stores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<ApplicationUser>()
                .HasOne<Store>()
                .WithOne(s => s.StoreOwner)
                .HasForeignKey<Store>(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);               
                
            base.OnModelCreating(builder);
        }
    }
}