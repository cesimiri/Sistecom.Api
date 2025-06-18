using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class ProductoDTO
    {

        public int IdProducto { get; set; }
        public string? CodigoPrincipal { get; set; } = null!;

        [Required(ErrorMessage = "El campo obligatorio")]
        public string? CodigoAuxiliar { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public int? IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public string TipoProducto { get; set; } = null!;

        public bool? EsComponente { get; set; }

        public bool? EsEnsamblable { get; set; }

        public bool? RequiereSerial { get; set; }

        //cambio 
        [Required(ErrorMessage = "El campo obligatorio")]
        public string? IdMarca { get; set; }

        //cambio
        [Required(ErrorMessage = "El campo obligatorio")]
        public string? IdModelo { get; set; }

        //cambio
        [Required(ErrorMessage = "El campo obligatorio")]
        public string? IdUnidadMedida { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public decimal PrecioUnitario { get; set; }

        public decimal? PrecioVentaSugerido { get; set; }

        public decimal? CostoEnsamblaje { get; set; }

        public int? TiempoEnsamblajeMinutos { get; set; }

        public bool? AplicaIva { get; set; }

        public decimal? PorcentajeIva { get; set; }

        public int? StockMinimo { get; set; }

        public int? StockMaximo { get; set; }

        public int? GarantiaMeses { get; set; }

        public string? EspecificacionesTecnicas { get; set; }

        public string? ImagenUrl { get; set; }

        public string? Estado { get; set; }

        //relacion 

        public string? NombreCategoria { get; set; }
    }
}
