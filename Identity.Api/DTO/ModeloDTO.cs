namespace Identity.Api.DTO
{
    public class ModeloDTO
    {
        public int IdModelo { get; set; }

        public int IdMarca { get; set; }

        public string? Codigo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int? AñoLanzamiento { get; set; }

        public bool? Descontinuado { get; set; }

        public DateTime? FechaDescontinuacion { get; set; }

        public string? EspecificacionesGenerales { get; set; }

        public string? ImagenUrl { get; set; }

        public string? Estado { get; set; }

        //relacion 
        public string? nombreMarca { get; set; }
    }
}
