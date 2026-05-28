using Microsoft.EntityFrameworkCore;
using SalonEventos.Data;

namespace SalonEventos.Repositorios
{
    public class RepositorioClientes(SalonEventosDBContext context) : IRepositorioClientes
    {
        readonly SalonEventosDBContext _context = context;

        public async Task AgregarCliente(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<Cliente?> ObtenerClientePorId(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task ActualizarCliente(int id, Cliente cliente)
        {
            var clienteExistente = _context.Clientes.Find(id);
            if (clienteExistente == null)
            {
                throw new Exception("Cliente no encontrado");
            }
            clienteExistente.NombreFestejado = cliente.NombreFestejado;
            clienteExistente.Telefono = cliente.Telefono;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarCliente(int id)
        {
            // NUEVO CANDADO: Revisar si existen eventos ligados a este cliente
            bool tieneEventos = await _context.Eventos.AnyAsync(e => e.ClienteId == id);

            if (tieneEventos)
            {
                // Si tiene eventos, detenemos el proceso y lanzamos la alerta
                throw new Exception("No se puede eliminar este Cliente porque aún tiene Eventos registrados a su nombre. Por favor, elimina primero sus eventos.");
            }

            // Si pasa la validación, borra al cliente de forma segura
            await _context.Clientes.Where(c => c.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}