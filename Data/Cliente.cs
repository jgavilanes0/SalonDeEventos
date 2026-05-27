using System.ComponentModel.DataAnnotations;

namespace SalonEventos.Data
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del festejado es obligatorio.")]
        public string NombreFestejado { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Length(10, 10, ErrorMessage = "El teléfono debe tener 10 dígitos.")]
        public string Telefono { get; set; } = string.Empty;

        virtual public List<Evento>? Eventos { get; set; }
    }
}
