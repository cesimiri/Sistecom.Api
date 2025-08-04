namespace Identity.Api.DTO
{
    public class CategoriasProductoDTO
    {
        public int IdCategoria { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool? RequiereSerial { get; set; }

        public int? VidaUtilMeses { get; set; }

        public string? Estado { get; set; }
    }
}
