using Microsoft.EntityFrameworkCore;
using Facturacion24.Models;

namespace Facturacion24.Data
{
    public class DbContexto : DbContext
    {
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Facturacion> Facturaciones { get; set; }
        public DbSet<AsientoContable> AsientosContables { get; set; }

        public DbContexto(DbContextOptions<DbContexto> options) : base(options)
        {
        }
    }
}
