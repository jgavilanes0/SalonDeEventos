using Microsoft.EntityFrameworkCore;
using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public class RepositorioMobiliarios(SalonEventosDBContext context) : IRepositorioMobiliarios
    {
        readonly SalonEventosDBContext _context = context;

        public async Task AgregarMobiliario(Mobiliario mobiliario)
        {
            await _context.Mobiliarios.AddAsync(mobiliario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Mobiliario>> ObtenerMobiliarios()
        {
            return await _context.Mobiliarios.AsNoTracking().ToListAsync();
        }

        public async Task<Mobiliario?> ObtenerMobiliarioPorId(int id)
        {
            return await _context.Mobiliarios.FindAsync(id);
        }

        public async Task ActualizarMobiliario(int id, Mobiliario mobiliario)
        {
            var mobiliarioExistente = _context.Mobiliarios.Find(id);
            if (mobiliarioExistente == null)
            {
                throw new Exception("Mobiliario no encontrado");
            }
            mobiliarioExistente.Nombre = mobiliario.Nombre;
            mobiliarioExistente.CantidadTotal = mobiliario.CantidadTotal;
            mobiliarioExistente.CantidadDisponible = mobiliario.CantidadDisponible;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarMobiliario(int id)
        {
            await _context.Mobiliarios.Where(m => m.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
