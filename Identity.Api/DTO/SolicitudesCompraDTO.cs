using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.DTO
{
    public class SolicitudesCompraDTO
    {
        public int IdSolicitud { get; set; }
        //automatica
        public string? NumeroSolicitud { get; set; } = null!;


        [Required(ErrorMessage = "El campo obligatorio")]
        public string RucEmpresa { get; set; } = null!;

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public int IdUsuarioSolicita { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        public int? IdUsuarioAutoriza { get; set; }

        //agregado
        [Required(ErrorMessage = "El campo obligatorio")]
        public int? IdUsuarioDestino { get; set; }


        [Required(ErrorMessage = "El campo obligatorio")]
        public DateTime FechaSolicitud { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public DateTime FechaRequerida { get; set; }

        //[Required(ErrorMessage = "El campo obligatorio")]
        public decimal SubtotalSinImpuestos { get; set; }

        public decimal? DescuentoTotal { get; set; }

        //[Required(ErrorMessage = "El campo obligatorio")]
        public decimal Iva { get; set; }

        //[Required(ErrorMessage = "El campo obligatorio")]
        public decimal ValorTotal { get; set; }

        public string? Justificacion { get; set; }

        //[Required(ErrorMessage = "El campo obligatorio")]
        //('BAJA', 'NORMAL', 'ALTA', 'URGENTE')
        public string? Prioridad { get; set; }

        [Required(ErrorMessage = "El campo obligatorio")]
        //('BORRADOR', 'ENVIADA', 'APROBADA', 'RECHAZADA', 'EN_PROCESO', 'COMPLETADA', 'CANCELADA')
        public string? Estado { get; set; }

        public string? MotivoRechazo { get; set; }

        public string? Observaciones { get; set; }

        public string? ArchivoOc { get; set; }


        //relacion 

        public string? RazonSocial { get; set; }
        public string? NombreSolicitanteCompleto { get; set; }
        public string? NombreAutorizadorCompleto { get; set; }

        public string? NombreDepartamento { get; set; }

    }
}
