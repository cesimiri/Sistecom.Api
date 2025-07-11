namespace Identity.Api.DTO
{
    public class stockBodegaDTO
    {
        public int IdStock { get; set; }
        public int IdBodega { get; set; }
        //public string NombreBodega { get; set; } = string.Empty;

        public int IdProducto { get; set; }
        //public string NombreProducto { get; set; } = string.Empty;

        public decimal CantidadDisponible { get; set; }
        public decimal? CantidadReservada { get; set; }
        public decimal? CantidadEnsamblaje { get; set; }
        public decimal? ValorPromedio { get; set; }
        public DateOnly? UltimaEntrada { get; set; }
        public DateOnly? UltimaSalida { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        //relacion
        public string? nombreBodega { get; set; }
        public string? nombreProducto { get; set; }

    }
}
