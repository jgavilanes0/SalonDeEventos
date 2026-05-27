using System.ComponentModel.DataAnnotations;

namespace SalonEventos.Data
{
    public class Sede
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        public string Ubicacion { get; set; } = string.Empty;

        public int Capacidad { get; set; }

        virtual public List<Evento>? Eventos { get; set; }
    }
}
