using Microsoft.EntityFrameworkCore;

namespace SalonEventos.Data
{
    public class SalonEventosDBContext(DbContextOptions<SalonEventosDBContext> options) : DbContext(options)
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Sede> Sedes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Mobiliario> Mobiliarios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<EventoServicio> EventoServicios { get; set; }
        public DbSet<EventoMobiliario> EventoMobiliarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventoServicio>()
                .HasKey(es => new { es.EventoId, es.ServicioId });

            modelBuilder.Entity<EventoMobiliario>()
                .HasKey(em => new { em.EventoId, em.MobiliarioId });
        }
    }
}
