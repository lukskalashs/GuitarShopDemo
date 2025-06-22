using GuitarShop.Data;
using GuitarShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

//Database Option 1: SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Database Option 2: SQLite 
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// route for sorting and paging category
app.MapControllerRoute(
        name: "sortingpagingcategory",
        pattern: "{controller}/{action}/{id}/orderby{sortBy}/page{productPage}");

// route for paging all products
app.MapControllerRoute(
    name: "allpaging",
    pattern: "{controller}/{action}/{id=all}/page{productPage}");

// route for sorting
app.MapControllerRoute(
    name: "sortingcategory",
    pattern: "{controller}/{action}/{id}/orderby{sortBy}");

// least specific route - 0 required segments 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

SeedData.EnsurePopulated(app);
SeedData.CreateAdminUser(app);

app.Run();
