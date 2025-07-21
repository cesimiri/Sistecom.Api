namespace Identity.Api.DTO
{
    public class FacturasCompraDTO
    {
        public int IdFactura { get; set; }

        public string NumeroFactura { get; set; } = null!;

        public string? NumeroAutorizacion { get; set; }

        public string? ClaveAcceso { get; set; }

        public int IdProveedor { get; set; }

        public int IdBodega { get; set; }

        public DateOnly FechaEmision { get; set; }

        public decimal SubtotalSinImpuestos { get; set; }

        public decimal? DescuentoTotal { get; set; }

        public decimal? Ice { get; set; }

        public decimal Iva { get; set; }

        public decimal ValorTotal { get; set; }

        public string? FormaPago { get; set; }

        public string? Estado { get; set; }

        public string? Observaciones { get; set; }

        //relacion 
        public string? RazonSocial { get; set; }
        public string? NombreBodega { get; set; }
        public string? NombreProveedor { get; set; }

    }
}
