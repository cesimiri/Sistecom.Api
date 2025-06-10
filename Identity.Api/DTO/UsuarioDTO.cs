namespace Identity.Api.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public int IdDepartamento { get; set; }

        public int IdCargo { get; set; }

        public string Cedula { get; set; } = null!;

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string? Email { get; set; }

        public string? Telefono { get; set; }

        public string? Extension { get; set; }

        public string? Estado { get; set; }

        
    }
}
