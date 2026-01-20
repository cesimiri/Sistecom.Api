namespace Identity.Api.DTO
{
    public class MantenimientoDTO
    {
        public int IdMantenimiento { get; set; }
        //requerido
        public int IdActivo { get; set; }
        //requerido
        public DateOnly FechaProgramada { get; set; }

        public DateOnly? FechaRealizada { get; set; }

        //('PREVENTIVO', 'CORRECTIVO', 'PREDICTIVO')
        public string TipoMantenimiento { get; set; } = null!;
        //requerido
        public string Descripcion { get; set; } = null!;

        public string? Diagnostico { get; set; }

        public string? AccionesRealizadas { get; set; }

        public string? RepuestosUsados { get; set; }

        public decimal? CostoManoObra { get; set; }

        public decimal? CostoRepuestos { get; set; }

        public decimal? CostoTotal { get; set; }

        public int? TiempoFueraServicioHoras { get; set; }

        public string? TecnicoResponsable { get; set; }

        public string? ProveedorServicio { get; set; }

        public string? NumeroOrdenServicio { get; set; }

        public int? GarantiaTrabajosDias { get; set; }

        public DateOnly? ProximoMantenimiento { get; set; }

        //('PROGRAMADO', 'EN_PROCESO', 'COMPLETADO', 'CANCELADO')
        public string? Estado { get; set; }

        public string? InformeTecnico { get; set; }
        //requerido
        public int? IdDepartamentoSolicita { get; set; }

        public string? CedulaTecnico { get; set; }

        //relacion 
        public string? ApellidosNombre { get; set; }
        public string? CodigoActivo { get; set; }

    }
}
