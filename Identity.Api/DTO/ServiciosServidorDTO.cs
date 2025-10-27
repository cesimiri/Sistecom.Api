namespace Identity.Api.DTO
{
    public class ServiciosServidorDTO
    {
        public int IdServicio { get; set; }

        public int IdServidor { get; set; }

        public string NombreServicio { get; set; } = null!;

        public string? TipoServicio { get; set; }

        public int? Puerto { get; set; }

        public string? Version { get; set; }

        public string? Estado { get; set; }

        public DateOnly? FechaInstalacion { get; set; }

        public string? Observaciones { get; set; }
        // relacion 
        public string? NombreServidor { get; set; }

    }
}
