using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class SucursaleDTO
    {

        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una empresa")]
        public string RucEmpresa { get; set; } = null!;

        public string CodigoSucursal { get; set; } = null!;

        [Required(ErrorMessage = "Debe tener un nombre")]
        public string NombreSucursal { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? Ciudad { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Responsable { get; set; }

        public string? TelefonoResponsable { get; set; }

        public bool? EsMatriz { get; set; }

        public string? Estado { get; set; }

        // relacion
        public string? RazonSocialEmpresa { get; set; }
    }
}
