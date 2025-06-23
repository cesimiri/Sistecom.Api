using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class DetalleSolicitudDTO
    {
        public int IdDetalle { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdSolicitud { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public decimal PrecioUnitario { get; set; }

        public decimal? Descuento { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public decimal Subtotal { get; set; }

        public string? Observaciones { get; set; }

        //relaciones



        public string? NumeroSolicitud { get; set; }

        public string? CodigoPrincipalsolicitud { get; set; }

    }
}
