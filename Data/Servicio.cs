using System.ComponentModel.DataAnnotations;

namespace SalonEventos.Data
{
    public class Servicio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        public decimal PrecioUnitario { get; set; }

        virtual public List<EventoServicio>? EventoServicios { get; set; }
    }
}
