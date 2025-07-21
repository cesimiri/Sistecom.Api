namespace Identity.Api.DTO
{
    public class ActivoDTO
    {
        public int IdActivo { get; set; }

        public string CodigoActivo { get; set; } = null!;

        public int IdProducto { get; set; }

        public string? NumeroSerie { get; set; }

        public string? NumeroParte { get; set; }

        public DateOnly FechaAdquisicion { get; set; }

        public DateOnly? FechaGarantiaFin { get; set; }

        public int? IdFacturaCompra { get; set; }

        public int? IdOrdenEnsamblaje { get; set; }

        public decimal ValorCompra { get; set; }

        public decimal? ValorResidual { get; set; }

        public int? VidaUtilMeses { get; set; }

        public string? UbicacionActual { get; set; }

        public string? EstadoActivo { get; set; }

        public string? CondicionFisica { get; set; }

        public bool? EsServidor { get; set; }

        public string? Observaciones { get; set; }


    }
}
