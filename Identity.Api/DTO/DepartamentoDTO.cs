using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class DepartamentoDTO
    {
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public string CodigoDepartamento { get; set; } = null!;

        public string NombreDepartamento { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? Responsable { get; set; }

        public string? Extension { get; set; }



        public string? Estado { get; set; }


        // REFERENCIA AL 
        public string? NombreSucursal { get; set; }
    }
}
