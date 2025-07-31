using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class UsuarioDetalleDTO
    {
        [Required(ErrorMessage = "El campo obligatorio")]
        public string Cedula { get; set; } = null!;

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdCargo { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        public DateTime? FechaBaja { get; set; }

        //'ACTIVO', 'INACTIVO', 'LICENCIA', 'VACACIONES'
        public string? Estado { get; set; }

        public string? Observaciones { get; set; }

        //relaciones 
        public string? NombreSucursal { get; set; }
        public string? NombreCargo { get; set; }
        public string? NombreDepartamento { get; set; }
        public string? NombreCedula { get; set; }


    }
}
