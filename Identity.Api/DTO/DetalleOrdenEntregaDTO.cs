namespace Identity.Api.DTO
{
    public class DetalleOrdenEntregaDTO
    {
        public int IdDetalle { get; set; }

        public int IdOrden { get; set; }

        public int IdProducto { get; set; }

        public decimal CantidadProgramada { get; set; }

        public decimal? CantidadEntregada { get; set; }

        public int? IdActivo { get; set; }

        public int? IdLicencia { get; set; }

        public string? Observaciones { get; set; }

        //relacion
        public string? NombreProducto { get; set; }


    }
}
