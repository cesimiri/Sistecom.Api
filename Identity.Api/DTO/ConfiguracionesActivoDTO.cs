namespace Identity.Api.DTO
{
    public class ConfiguracionesActivoDTO
    {
        public int IdConfiguracion { get; set; }

        public int IdActivoPrincipal { get; set; }

        public int IdComponente { get; set; }

        public DateOnly FechaInstalacion { get; set; }

        public DateOnly? FechaRemocion { get; set; }

        public string? UbicacionFisica { get; set; }

        public string? EstadoConfiguracion { get; set; }

        public string? Observaciones { get; set; }

        public string? CedulaTecnico { get; set; }

        //relacion 

        public string? CodigoActivo { get; set; } // Código del principal
        public string? CodigoActivoComponente { get; set; } // Código del componente
        public string? NombreProducto { get; set; }
        public string? NumeroSerie { get; set; }
    }

    // DTO para insertar varios componentes
    public class ConfiguracionActivoInsertDTO
    {
        public int IdActivoPrincipal { get; set; }
        public List<ConfiguracionesActivoDTO> Componentes { get; set; } = new();
    }
}
