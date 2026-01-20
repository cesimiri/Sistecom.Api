namespace Identity.Api.DTO
{
    public class HistorialActivoDTO
    {
        public int IdHistorial { get; set; }

        public int IdActivo { get; set; }

        public string TipoEvento { get; set; } = null!;

        public DateTime FechaEvento { get; set; }

        public string? Descripcion { get; set; }

        public string? CedulaResponsable { get; set; }

        public int? IdDepartamentoEvento { get; set; }

        public int? IdDocumentoReferencia { get; set; }

        public decimal? CostoAsociado { get; set; }
    }
}
