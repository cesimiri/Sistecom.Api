namespace Identity.Api.DTO
{
    public class DetalleFacturaCompraDTO
    {
        public int IdDetalle { get; set; }

        public int IdFactura { get; set; }

        public int IdProducto { get; set; }

        public decimal Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal? Descuento { get; set; }

        public decimal Subtotal { get; set; }

        public string? NumerosSerie { get; set; }

        public string? DetallesAdicionales { get; set; }
    }
}
