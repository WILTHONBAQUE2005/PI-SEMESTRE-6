using Microsoft.EntityFrameworkCore;

namespace SwimRoomWeb.Models
{
    public class AplicacionDbContext : DbContext
    {
        public AplicacionDbContext(DbContextOptions<AplicacionDbContext> options)
            : base(options)
        {
        }

        public DbSet<VentaDiferida> VentasDiferidas { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
