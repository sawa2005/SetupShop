using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetupShop.Areas.Identity.Data;

namespace SetupShop.Data;

public class SetupShopContext : IdentityDbContext<SetupShopUser>
{
    public SetupShopContext(DbContextOptions<SetupShopContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new SetupShopUserEntityConfiguration());
    }
}

public class SetupShopUserEntityConfiguration : IEntityTypeConfiguration<SetupShopUser>
{
    public void Configure(EntityTypeBuilder<SetupShopUser> builder)
    {
        builder.Property(u => u.DisplayName).HasMaxLength(128);
    }
}
