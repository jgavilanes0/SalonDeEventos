using Microsoft.EntityFrameworkCore;
using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public class RepositorioEventos(SalonEventosDBContext context) : IRepositorioEventos
    {
        readonly SalonEventosDBContext _context = context;

        public async Task AgregarEvento(Evento evento)
        {
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
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task ActualizarEvento(int id, Evento evento)
        {
            var eventoExistente = _context.Eventos.Find(id);
            if (eventoExistente == null)
            {
                throw new Exception("Evento no encontrado");
            }
            eventoExistente.Fecha = evento.Fecha;
            eventoExistente.TipoEvento = evento.TipoEvento;
            eventoExistente.ClienteId = evento.ClienteId;
            eventoExistente.SedeId = evento.SedeId;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarEvento(int id)
        {
            await _context.Eventos.Where(e => e.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
