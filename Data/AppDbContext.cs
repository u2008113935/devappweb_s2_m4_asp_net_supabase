using Microsoft.EntityFrameworkCore;
using PortalPagos.Models;

namespace PortalPagos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pago> Pagos { get; set; }
    }
}