namespace SalonEventos.Data
{
    public class EventoServicio
    {
        public int EventoId { get; set; }
        virtual public Evento? Evento { get; set; }

        public int ServicioId { get; set; }
        virtual public Servicio? Servicio { get; set; }

        public int Cantidad { get; set; }
    }
}
