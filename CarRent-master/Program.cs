using CarRent.Areas.Identity.Data;
using CarRent.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// SQLite bağlantı dizesi
var connectionString = builder.Configuration.GetConnectionString("CarRentDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'CarRentDbContextConnection' not found.");
builder.Services.AddDbContext<CarRentDbContext>(options => options.UseSqlite(connectionString));

// Identity ve AppUser bağımlılıkları ekleniyor
builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // E-posta onayı gereksinimini kapatma
})
.AddEntityFrameworkStores<CarRentDbContext>();

// Diğer veritabanı bağlamı için yapılandırma (CarDbContext)
builder.Services.AddDbContext<CarDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CarDbContext")
    ?? throw new InvalidOperationException("Connection string 'CarDbContext' not found.")));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
