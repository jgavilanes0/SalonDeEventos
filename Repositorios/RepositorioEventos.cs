using Microsoft.EntityFrameworkCore;
using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public class RepositorioEventos(SalonEventosDBContext context) : IRepositorioEventos
    {
        readonly SalonEventosDBContext _context = context;

        public async Task AgregarEvento(Evento evento)
        {
            // 1. Restar el mobiliario de la bodega
            if (evento.EventoMobiliarios != null)
            {
                foreach (var em in evento.EventoMobiliarios)
                {
                    var mob = await _context.Mobiliarios.FindAsync(em.MobiliarioId);
                    if (mob != null) mob.CantidadDisponible -= em.CantidadReservada;
                }
            }

            // El Entity Framework detectará automáticamente los EventoServicios añadidos al objeto evento
            await _context.Eventos.AddAsync(evento);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Evento>> ObtenerEventos()
        {
            return await _context.Eventos
                .AsNoTracking()
                .Include(e => e.Cliente)
                .Include(e => e.Sede)
                .ToListAsync();
        }

        public async Task<Evento?> ObtenerEventoPorId(int id)
        {
            return await _context.Eventos
                .Include(e => e.Cliente)
                .Include(e => e.Sede)
                .Include(e => e.EventoMobiliarios)
                .Include(e => e.EventoServicios) // <-- Incluimos los servicios
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task ActualizarEvento(int id, Evento evento)
        {
            var eventoExistente = await _context.Eventos
                .Include(e => e.EventoMobiliarios)
                .Include(e => e.EventoServicios) // <-- Incluimos los servicios
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventoExistente == null) throw new Exception("Evento no encontrado");

            // --- LÓGICA DE MOBILIARIO (Inventario) ---
            if (eventoExistente.EventoMobiliarios != null)
            {
                foreach (var emViejo in eventoExistente.EventoMobiliarios)
                {
                    var mob = await _context.Mobiliarios.FindAsync(emViejo.MobiliarioId);
                    if (mob != null) mob.CantidadDisponible += emViejo.CantidadReservada;
                }
            }

            // --- ACTUALIZAR DATOS BÁSICOS ---
            eventoExistente.Fecha = evento.Fecha;
            eventoExistente.TipoEvento = evento.TipoEvento;
            eventoExistente.ClienteId = evento.ClienteId;
            eventoExistente.SedeId = evento.SedeId;

            // --- ACTUALIZAR MOBILIARIO (Relación) ---
            eventoExistente.EventoMobiliarios!.Clear();
            if (evento.EventoMobiliarios != null)
            {
                foreach (var em in evento.EventoMobiliarios)
                {
                    eventoExistente.EventoMobiliarios.Add(new EventoMobiliario
                    {
                        MobiliarioId = em.MobiliarioId,
                        CantidadReservada = em.CantidadReservada
                    });
                    var mob = await _context.Mobiliarios.FindAsync(em.MobiliarioId);
                    if (mob != null) mob.CantidadDisponible -= em.CantidadReservada;
                }
            }

            // --- ACTUALIZAR SERVICIOS (Relación N:M) ---
            eventoExistente.EventoServicios!.Clear();
            if (evento.EventoServicios != null)
            {
                foreach (var es in evento.EventoServicios)
                {
                    eventoExistente.EventoServicios.Add(new EventoServicio
                    {
                        ServicioId = es.ServicioId,
                        Cantidad = es.Cantidad
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task EliminarEvento(int id)
        {
            var eventoExistente = await _context.Eventos
                .Include(e => e.EventoMobiliarios)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventoExistente != null)
            {
                if (eventoExistente.EventoMobiliarios != null)
                {
                    foreach (var em in eventoExistente.EventoMobiliarios)
                    {
                        var mob = await _context.Mobiliarios.FindAsync(em.MobiliarioId);
                        if (mob != null) mob.CantidadDisponible += em.CantidadReservada;
                    }
                }
                _context.Eventos.Remove(eventoExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteEventoEnSedeYFecha(int sedeId, DateTime fecha, int eventoIdExcluir)
        {
            return await _context.Eventos.AnyAsync(e =>
                e.SedeId == sedeId &&
                e.Fecha.Date == fecha.Date &&
                e.Id != eventoIdExcluir);
        }
    }
}