using Microsoft.EntityFrameworkCore;
using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public class RepositorioPagos(SalonEventosDBContext context) : IRepositorioPagos
    {
        readonly SalonEventosDBContext _context = context;

        public async Task AgregarPago(Pago pago)
        {
            await _context.Pagos.AddAsync(pago);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Pago>> ObtenerPagos()
        {
            return await _context.Pagos
                .AsNoTracking()
                .Include(p => p.Evento)
                .ToListAsync();
        }

        public async Task<Pago?> ObtenerPagoPorId(int id)
        {
            return await _context.Pagos
                .Include(p => p.Evento)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task ActualizarPago(int id, Pago pago)
        {
            var pagoExistente = _context.Pagos.Find(id);
            if (pagoExistente == null)
            {
                throw new Exception("Pago no encontrado");
            }
            pagoExistente.Estado = pago.Estado;
            pagoExistente.MontoTotal = pago.MontoTotal;
            pagoExistente.Anticipo = pago.Anticipo;
            pagoExistente.EventoId = pago.EventoId;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarPago(int id)
        {
            await _context.Pagos.Where(p => p.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
