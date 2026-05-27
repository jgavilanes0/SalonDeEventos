using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public interface IRepositorioSedes
    {
        Task AgregarSede(Sede sede);
        Task<List<Sede>> ObtenerSedes();
        Task<Sede?> ObtenerSedePorId(int id);
        Task ActualizarSede(int id, Sede sede);
        Task EliminarSede(int id);
    }
}
