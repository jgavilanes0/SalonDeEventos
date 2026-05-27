using Microsoft.EntityFrameworkCore;
using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public class RepositorioServicios(SalonEventosDBContext context) : IRepositorioServicios
    {
        readonly SalonEventosDBContext _context = context;

        public async Task AgregarServicio(Servicio servicio)
        {
            await _context.Servicios.AddAsync(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Servicio>> ObtenerServicios()
        {
            return await _context.Servicios.AsNoTracking().ToListAsync();
        }

        public async Task<Servicio?> ObtenerServicioPorId(int id)
        {
            return await _context.Servicios.FindAsync(id);
        }

        public async Task ActualizarServicio(int id, Servicio servicio)
        {
            var servicioExistente = _context.Servicios.Find(id);
            if (servicioExistente == null)
            {
                throw new Exception("Servicio no encontrado");
            }
            servicioExistente.Nombre = servicio.Nombre;
            servicioExistente.PrecioUnitario = servicio.PrecioUnitario;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarServicio(int id)
        {
            await _context.Servicios.Where(s => s.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
