namespace Identity.Api.DTO
{
    public class ActivoDTO
    {

        public int? IdActivo { get; set; }

        public string? CodigoActivo { get; set; } // ahora es opcional

        public int IdProducto { get; set; }

        public string? NumeroSerie { get; set; }

        public string? NumeroParte { get; set; }

        public DateTime FechaAdquisicion { get; set; }

        public DateTime? FechaGarantiaFin { get; set; }

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

        public DateTime? FechaRegistro { get; set; }

        //nuevos campos
        public int? IdActivoPadre { get; set; }

        public bool? EsComponente { get; set; }

        public string? TipoRelacion { get; set; }

        public string? OldInv { get; set; }

        //relaciones 
        public string? NombreProducto { get; set; }
        public string? NumeroFactura { get; set; }
        public string? NumeroOrden { get; set; }
        public int IdCategoriaProducto { get; set; }
    }
}
