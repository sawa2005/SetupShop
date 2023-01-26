using Microsoft.EntityFrameworkCore;
using SetupShop.Data;
using Microsoft.AspNetCore.Identity;
using SetupShop.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database connections
builder.Services.AddDbContext<SetupContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultDbString"))
);

builder.Services.AddDbContext<SetupShopContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultDbString"))
);

builder.Services.AddDefaultIdentity<SetupShopUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SetupShopContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
