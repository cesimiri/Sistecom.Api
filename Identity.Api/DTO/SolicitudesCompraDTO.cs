using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class SolicitudesCompraDTO
    {
        public int IdSolicitud { get; set; }
        //automatico
        public string? NumeroSolicitud { get; set; } = null!;

        [Required(ErrorMessage = "El campo obligatorio")]
        public string RucEmpresa { get; set; } = null!;

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdDepartamento { get; set; }

        // Cambiados de ID a CÉDULAS
        [Required(ErrorMessage = "El campo obligatorio")]
        public string CedulaSolicita { get; set; } = null!;

        [Required(ErrorMessage = "El campo obligatorio")]
        public string? CedulaAutoriza { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public string? CedulaDestino { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public DateTime FechaSolicitud { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public DateOnly? FechaRequerida { get; set; }

        public decimal SubtotalSinImpuestos { get; set; }

        public decimal? DescuentoTotal { get; set; }

        public decimal Iva { get; set; }

        public decimal ValorTotal { get; set; }

        public string? Justificacion { get; set; }

        public string? Prioridad { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public string? Estado { get; set; }

        public string? MotivoRechazo { get; set; }

        public string? Observaciones { get; set; }

        public string? ArchivoOc { get; set; }

        public DateTime? FechaRegistro { get; set; }

        // CAMPOS DE NAVEGACIÓN (relaciones, solo lectura o presentación)
        public string? NombreSolicitanteCompleto { get; set; }

        public string? NombreAutorizadorCompleto { get; set; }

        public string? NombreDepartamento { get; set; }

        public string? RazonSocial { get; set; }

    }
}
