namespace SalonEventos.Data
{
    public class EventoMobiliario
    {
        public int EventoId { get; set; }
        virtual public Evento? Evento { get; set; }

        public int MobiliarioId { get; set; }
        virtual public Mobiliario? Mobiliario { get; set; }

        public int CantidadReservada { get; set; }
    }
}
