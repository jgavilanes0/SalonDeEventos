using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public interface IRepositorioEventos
    {
        Task AgregarEvento(Evento evento);
        Task<List<Evento>> ObtenerEventos();
        Task<Evento?> ObtenerEventoPorId(int id);
        Task ActualizarEvento(int id, Evento evento);
        Task EliminarEvento(int id);
    }
}
