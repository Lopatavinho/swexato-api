using Microsoft.EntityFrameworkCore;
using Swexato.Api.Models;

namespace Swexato.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
