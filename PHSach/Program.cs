using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PHSach.Models;

var builder = WebApplication.CreateBuilder(args);

// ===== Cấu hình EF Core với SQL Server =====
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ===== Cấu hình Cookie Authentication =====
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login";          // Redirect khi chưa đăng nhập
        options.LogoutPath = "/Account/Logout";        // Đường dẫn logout
        options.AccessDeniedPath = "/Account/AccessDenied"; // Khi bị chặn quyền
        options.ExpireTimeSpan = TimeSpan.FromHours(1);     // Thời gian sống của cookie
        options.SlidingExpiration = true;                   // Tự động gia hạn nếu còn hoạt động
    });

builder.Services.AddAuthorization();

// ===== Cấu hình MVC (Controllers + Views) =====
builder.Services.AddControllersWithViews();

// ===== EPPlus License Context =====
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var app = builder.Build();

// ===== Middleware Pipeline =====
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Bảo mật Authentication + Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
