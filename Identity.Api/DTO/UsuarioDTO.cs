using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdCargo { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public string Cedula { get; set; } = null!;

        [Required(ErrorMessage = "El campo obligatorio")]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El campo obligatorio")]
        public string Apellidos { get; set; } = null!;

        public string? Email { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public string? Telefono { get; set; }

        public string? Extension { get; set; }

        public bool? PuedeSolicitar { get; set; }

        public string? Estado { get; set; }

        //relacion

        public string? NombreEmpresa { get; set; }
        public string? nombreSucursal { get; set; }
        public string? NombreDepartamento { get; set; }


        public string? NombreCargo { get; set; }



    }
}
