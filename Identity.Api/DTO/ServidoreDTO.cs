namespace Identity.Api.DTO
{
    public class ServidoreDTO
    {
        public int IdServidor { get; set; }
        public int IdActivo { get; set; }

        public string NombreServidor { get; set; } = null!;

        //('FISICO', 'VIRTUAL', 'CLOUD')
        public string TipoServidor { get; set; } = null!;

        public string? SistemaOperativo { get; set; }

        public string? VersionSo { get; set; }

        public int? Procesadores { get; set; }

        public int? NucleosPorProcesador { get; set; }

        public int? MemoriaRamGb { get; set; }

        public decimal? AlmacenamientoTb { get; set; }

        public string? DireccionIp { get; set; }

        public string? DireccionMac { get; set; }

        public string? Virtualizacion { get; set; }

        public string? HostFisico { get; set; }

        public string? UbicacionRack { get; set; }

        public string? Proposito { get; set; }

        //('ACTIVO', 'INACTIVO', 'MANTENIMIENTO', 'BAJA')
        public string? Estado { get; set; }

        //relacion
        public string? NombreProducto { get; set; }
        public string? NumeroSerie { get; set; }


    }
}
