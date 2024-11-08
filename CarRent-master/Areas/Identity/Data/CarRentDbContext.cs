using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarRent.Areas.Identity.Data;

namespace CarRent.Data
{
    public class CarRentDbContext : IdentityDbContext<AppUser>
    {
        public CarRentDbContext(DbContextOptions<CarRentDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Identity tablolarını oluşturmak için gerekli
        }
    }
}
