namespace Identity.Api.DTO
{
    public class MarcaDTO
    {
        public int IdMarca { get; set; }

        public string? Codigo { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? PaisOrigen { get; set; }

        public string? SitioWeb { get; set; }

        public string? LogoUrl { get; set; }

        public bool? EsMarcaPropia { get; set; }

        public string? Estado { get; set; }
    }
}
