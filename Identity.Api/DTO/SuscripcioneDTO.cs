using System.ComponentModel.DataAnnotations;

namespace Identity.Api.DTO
{
    public class SuscripcionDto
    {
        public int IdSuscripcion { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una empresa")]
        public string RucEmpresa { get; set; } = null!;

        [Required(ErrorMessage = "Debe seleccionar una Proveedor")]
        public int IdProveedor { get; set; }
        public string NombreServicio { get; set; } = null!;
        public string? TipoSuscripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaRenovacion { get; set; }
        
        
        //agregados
        public string Certificado { get; set; }
        public string Dominio { get; set; }
        public string Soporte { get;set; }
        //


        public string? PeriodoFacturacion { get; set; }
        public decimal CostoPeriodo { get; set; }
        public int? UsuariosIncluidos { get; set; }
        public int? AlmacenamientoGb { get; set; }
        public string? UrlAcceso { get; set; }
        public string? Administrador { get; set; }
        public string? Estado { get; set; }
        public int? NotificarDiasAntes { get; set; }
        public string? Observaciones { get; set; }

        public string? RazonSocialProveedor { get; set; }
        public string? RazonSocialEmpresa { get; set; }

    }

}

