using System.ComponentModel.DataAnnotations;

namespace SalonEventos.Data
{
    public class Mobiliario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        public int CantidadTotal { get; set; }
        public int CantidadDisponible { get; set; }

        virtual public List<EventoMobiliario>? EventoMobiliarios { get; set; }
    }
}
