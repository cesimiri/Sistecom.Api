namespace Identity.Api.DTO
{
    public class AsignacionesLicenciaDTO
    {
        public int IdAsignacionLicencia { get; set; }

        public int IdLicencia { get; set; }

        public int? IdActivo { get; set; }

        public int? IdServidor { get; set; }

        public DateOnly FechaAsignacion { get; set; }

        public DateOnly? FechaDesasignacion { get; set; }

        public string TipoAsignacion { get; set; } = null!;

        public string? Estado { get; set; }

        public string? Observaciones { get; set; }

        public string? CedulaUsuario { get; set; }

        public int? IdDepartamentoUsuario { get; set; }

        //relacion
        public string? NombreTipoLicencia { get; set; }
        public string? NombreProducto { get; set; }
        public string? NombreDepartamento { get; set; }
        public string? NombreActivo { get; set; }
        public string? NombresApellidos { get; set; }


    }
}
