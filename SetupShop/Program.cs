using Microsoft.EntityFrameworkCore;
using SetupShop.Data;
using Microsoft.AspNetCore.Identity;
using SetupShop.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential= true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

AddAuthorizationPolicies(builder.Services);

// Database connections
builder.Services.AddDbContext<SetupShopContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultDbString"))
);

builder.Services.AddDefaultIdentity<SetupShopUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SetupShopContext>();

var app = builder.Build();

app.UseSession();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<SetupShopContext>();
SeedData.SeedDatabase(context);

app.Run();

void AddAuthorizationPolicies(IServiceCollection services)
{
    services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAuthor", policy => policy.RequireRole("Author"));
    });
}
