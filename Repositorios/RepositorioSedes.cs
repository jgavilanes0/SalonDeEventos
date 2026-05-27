using Microsoft.EntityFrameworkCore;
using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public class RepositorioSedes(SalonEventosDBContext context) : IRepositorioSedes
    {
        readonly SalonEventosDBContext _context = context;

        public async Task AgregarSede(Sede sede)
        {
            await _context.Sedes.AddAsync(sede);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Sede>> ObtenerSedes()
        {
            return await _context.Sedes.AsNoTracking().ToListAsync();
        }

        public async Task<Sede?> ObtenerSedePorId(int id)
        {
            return await _context.Sedes.FindAsync(id);
        }

        public async Task ActualizarSede(int id, Sede sede)
        {
            var sedeExistente = _context.Sedes.Find(id);
            if (sedeExistente == null)
            {
                throw new Exception("Sede no encontrada");
            }
            sedeExistente.Nombre = sede.Nombre;
            sedeExistente.Ubicacion = sede.Ubicacion;
            sedeExistente.Capacidad = sede.Capacidad;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarSede(int id)
        {
            await _context.Sedes.Where(s => s.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
