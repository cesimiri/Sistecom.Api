using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class BodegaDTO
    {
        public int IdBodega { get; set; }


        public string? Codigo { get; set; } = null!;


        [Required(ErrorMessage = "El campo obligatorio")]
        public string Nombre { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Responsable { get; set; }

        //COMBO BOX ('PRINCIPAL', 'SECUNDARIA', 'TRANSITO', 'CUARENTENA', 'TALLER')
        public string? Tipo { get; set; }

        public bool PermiteVentas { get; set; }

        public bool PermiteEnsamblaje { get; set; }

        //COMBO BOX ('ACTIVO', 'INACTIVO')
        public string? Estado { get; set; }
    }
}
