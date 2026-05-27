using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public interface IRepositorioMobiliarios
    {
        Task AgregarMobiliario(Mobiliario mobiliario);
        Task<List<Mobiliario>> ObtenerMobiliarios();
        Task<Mobiliario?> ObtenerMobiliarioPorId(int id);
        Task ActualizarMobiliario(int id, Mobiliario mobiliario);
        Task EliminarMobiliario(int id);
    }
}
