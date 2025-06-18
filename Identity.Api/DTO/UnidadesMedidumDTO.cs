namespace Identity.Api.DTO
{
    public class UnidadesMedidumDTO
    {
        public int IdUnidadMedida { get; set; }

        //secuencial
        public string? Codigo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool PermiteDecimales { get; set; }

        public bool EsUnidadBase { get; set; }

        public decimal? FactorConversion { get; set; }

        public int? IdUnidadBase { get; set; }

        public string? Estado { get; set; }
    }
}
