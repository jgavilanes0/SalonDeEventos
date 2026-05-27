using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public interface IRepositorioPagos
    {
        Task AgregarPago(Pago pago);
        Task<List<Pago>> ObtenerPagos();
        Task<Pago?> ObtenerPagoPorId(int id);
        Task ActualizarPago(int id, Pago pago);
        Task EliminarPago(int id);
    }
}
