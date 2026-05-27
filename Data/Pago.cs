using System.ComponentModel.DataAnnotations;

namespace SalonEventos.Data
{
    public class Pago
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; } = string.Empty;

        public decimal MontoTotal { get; set; }
        public decimal Anticipo { get; set; }

        public int EventoId { get; set; }
        virtual public Evento? Evento { get; set; }
    }
}
