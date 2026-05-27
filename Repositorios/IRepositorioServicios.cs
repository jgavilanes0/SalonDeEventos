using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public interface IRepositorioServicios
    {
        Task AgregarServicio(Servicio servicio);
        Task<List<Servicio>> ObtenerServicios();
        Task<Servicio?> ObtenerServicioPorId(int id);
        Task ActualizarServicio(int id, Servicio servicio);
        Task EliminarServicio(int id);
    }
}
