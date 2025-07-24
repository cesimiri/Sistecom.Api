using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class UsuarioDTO
    {

        public string Cedula { get; set; } = null!;

        [Required(ErrorMessage = "El campo obligatorio")]
        public string Nombres { get; set; } = null!;
        [Required(ErrorMessage = "El campo obligatorio")]
        public string Apellidos { get; set; } = null!;

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Extension { get; set; }
        //('ACTIVO', 'INACTIVO')
        public string? Estado { get; set; }


    }
}
