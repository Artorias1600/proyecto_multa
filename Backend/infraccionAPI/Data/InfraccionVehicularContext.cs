using infraccionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace infraccionAPI.Data
{
    public class InfraccionVehicularContext : DbContext
    {
        public InfraccionVehicularContext(DbContextOptions<InfraccionVehicularContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Sancion> Sanciones { get; set; }
        public DbSet<AgenteTransito> AgentesTransito { get; set; }
        public DbSet<Infraccion> Infracciones { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(u => u.UsuarioID);
            modelBuilder.Entity<Vehiculo>().HasKey(v => v.VehiculoID);
            modelBuilder.Entity<Conductor>().HasKey(c => c.ConductorID);
            modelBuilder.Entity<Sancion>().HasKey(s => s.SancionID);
            modelBuilder.Entity<AgenteTransito>().HasKey(a => a.AgenteID);
            modelBuilder.Entity<Infraccion>().HasKey(i => i.InfraccionID);
            modelBuilder.Entity<Pago>().HasKey(p => p.PagoID);

            modelBuilder.Entity<Infraccion>()
                .HasOne(i => i.Vehiculo)
                .WithMany()
                .HasForeignKey(i => i.VehiculoID);

            modelBuilder.Entity<Infraccion>()
                .HasOne(i => i.Conductor)
                .WithMany()
                .HasForeignKey(i => i.ConductorID);

            modelBuilder.Entity<Infraccion>()
                .HasOne(i => i.Sancion)
                .WithMany()
                .HasForeignKey(i => i.SancionID);

            modelBuilder.Entity<Infraccion>()
                .HasOne(i => i.AgenteTransito)
                .WithMany()
                .HasForeignKey(i => i.AgenteID);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Infraccion)
                .WithMany()
                .HasForeignKey(p => p.InfraccionID);
        }
    }
}
