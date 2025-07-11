namespace Identity.Api.DTO
{
    public class MovimientosInventarioDTO
    {
        public int IdMovimiento { get; set; }

        public int IdBodega { get; set; }

        public int IdProducto { get; set; }

        public string TipoMovimiento { get; set; } = null!;

        public string? Origen { get; set; }

        public int? IdDocumentoOrigen { get; set; }

        public int? IdBodegaOrigen { get; set; }

        public int? IdBodegaDestino { get; set; }

        public decimal Cantidad { get; set; }

        public decimal? PrecioUnitario { get; set; }

        public string? NumeroSerie { get; set; }

        public decimal? StockAnterior { get; set; }

        public decimal? StockActual { get; set; }

        public string? Observaciones { get; set; }

        public string? UsuarioRegistro { get; set; }

        public DateTime? FechaMovimiento { get; set; }

        //relacion
        public string? NombreProducto { get; set; }
        public string? NombreBodega { get; set; }

    }
}
