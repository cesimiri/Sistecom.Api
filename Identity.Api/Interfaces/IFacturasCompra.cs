using Identity.Api.DTO;
using Identity.Api.Paginado;

namespace Identity.Api.Interfaces
{
    public interface IFacturasCompra
    {
        IEnumerable<FacturasCompraDTO> FacturasCompraInfoAll { get; }
        FacturasCompraDTO GetFacturasCompraById(int idFacturasCompra);
        int InsertFacturasCompra(FacturasCompraDTO New);
        //por enventos en el front solo en update
        Task UpdateFacturasCompra(FacturasCompraDTO UpdItem);

        //void DeleteFacturasCompra(FacturasCompra DelItem);
        void DeleteFacturasCompraById(int IdFacturasCompra);


        // Nuevo método para paginado:
        PagedResult<FacturasCompraDTO> GetFacturasCompraPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);

        //exportar
        List<FacturasCompraDTO> ObtenerFacturaCompraFiltradas(string? filtro, string? estado);




        //PARA FACTURA AUTOMATICA
        Task<(FacturasCompraDTO factura, List<DetalleFacturaCompraDTO> detalles)> ObtenerFacturaConDetallesAsync(int idFactura);


    }
}
