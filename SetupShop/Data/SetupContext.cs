using SetupShop.Models;
using Microsoft.EntityFrameworkCore;

namespace SetupShop.Data
{
    public class SetupContext : DbContext
    {
        public SetupContext(DbContextOptions<SetupContext> options) : base(options) {}

        public DbSet<Setup> Setup { get; set; }
    }
}
