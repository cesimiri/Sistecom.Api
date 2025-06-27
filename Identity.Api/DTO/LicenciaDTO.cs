namespace Identity.Api.DTO
{
    public class LicenciaDTO
    {
        public int IdLicencia { get; set; }

        public int IdTipoLicencia { get; set; }

        public int? IdProducto { get; set; }

        public int? IdFacturaCompra { get; set; }

        public string NumeroLicencia { get; set; } = null!;

        public string? ClaveProducto { get; set; }

        public DateOnly FechaAdquisicion { get; set; }

        public DateOnly? FechaInicioVigencia { get; set; }

        public DateOnly? FechaFinVigencia { get; set; }

        public string? TipoSuscripcion { get; set; }

        public int? CantidadUsuarios { get; set; }

        public decimal? CostoLicencia { get; set; }

        public bool? RenovacionAutomatica { get; set; }

        public string? Observaciones { get; set; }

        public string? Estado { get; set; }

        //relacion 

        public string? nombreLicencia { get; set; }

        public string? nombreProducto { get; set; }

        public string numeroFactura {  get; set; }
    }
}
