namespace Identity.Api.DTO
{
    public class stockBodegaDTO
    {
        public int IdStock { get; set; }

        public int IdBodega { get; set; }

        public int IdProducto { get; set; }

        public decimal CantidadDisponible { get; set; }

        public decimal? CantidadReservada { get; set; }

        public decimal? CantidadEnsamblaje { get; set; }

        public decimal? ValorPromedio { get; set; }

        //relacion 

        public string? nombreBodega { get; set; }

        public string? nombreProducto { get; set; }


    }
}
