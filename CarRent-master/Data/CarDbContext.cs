using Microsoft.EntityFrameworkCore;
using CarRent.Models;
using CarRent.Areas.Identity.Data;

namespace CarRent.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Car { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<CartViewModel> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().Ignore(c => c.Car);
            modelBuilder.Entity<CartViewModel>().HasNoKey();

            modelBuilder.Entity<CartViewModel>()
                .Property(c => c.TotalPrice)
                .HasColumnType("REAL");

            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalPrice)
                .HasColumnType("REAL");

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.DailyRate)
                .HasColumnType("REAL");

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.Discount)
                .HasColumnType("REAL");

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.Penalty)
                .HasColumnType("REAL");

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.TotalPrice)
                .HasColumnType("REAL");

            modelBuilder.Entity<Car>()
                .Property(c => c.PhotoPath)
                .HasColumnType("TEXT");

            modelBuilder.Entity<Car>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Car)
                .HasForeignKey(ci => ci.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.CartItems)
                .WithOne(ci => ci.User)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
