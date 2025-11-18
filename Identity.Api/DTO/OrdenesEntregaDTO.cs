namespace Identity.Api.DTO
{
    public class OrdenesEntregaDTO
    {
        public int IdOrden { get; set; }

        public string? NumeroOrden { get; set; }

        public int IdSolicitud { get; set; }

        public string? CedulaRecibe { get; set; }

        public DateOnly FechaProgramada { get; set; }

        //public string? HoraProgramada { get; set; }

        public DateTime? FechaEntrega { get; set; }

        public string? DireccionEntrega { get; set; }

        public string? ContactoRecepcion { get; set; }

        public string? TelefonoContacto { get; set; }

        public string? Estado { get; set; }

        public string? GuiaRemision { get; set; }

        public string? Transportista { get; set; }

        public string? FirmaRecepcion { get; set; }

        //public string? FotoEntrega { get; set; }

        public bool? IncluyeLicencias { get; set; }

        public string? ObservacionesEntrega { get; set; }

        public DateTime? FechaRegistro { get; set; }



        public int? IdDepartamentoEntrega { get; set; }

        //Relacion
        public string? NombreDepartamento { get; set; }
        public string? NumeroSolicitud { get; set; }

    }
}
