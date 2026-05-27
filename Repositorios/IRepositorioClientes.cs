using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public interface IRepositorioClientes
    {
        Task AgregarCliente(Cliente cliente);
        Task<List<Cliente>> ObtenerClientes();
        Task<Cliente?> ObtenerClientePorId(int id);
        Task ActualizarCliente(int id, Cliente cliente);
        Task EliminarCliente(int id);
    }
}
