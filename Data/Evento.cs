using System.ComponentModel.DataAnnotations;

namespace SalonEventos.Data
{
    public class Evento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El tipo de evento es obligatorio.")]
        public string TipoEvento { get; set; } = string.Empty;

        public int ClienteId { get; set; }
        virtual public Cliente? Cliente { get; set; }

        public int SedeId { get; set; }
        virtual public Sede? Sede { get; set; }

        virtual public List<Pago>? Pagos { get; set; }
        virtual public List<EventoServicio>? EventoServicios { get; set; }
        virtual public List<EventoMobiliario>? EventoMobiliarios { get; set; }
    }
}
