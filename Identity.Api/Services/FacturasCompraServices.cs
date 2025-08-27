using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
namespace Identity.Api.Services
{

    public class FacturasCompraServices : IFacturasCompra
    {
        private FacturasCompraRepository _dataRepository = new FacturasCompraRepository();

        public IEnumerable<FacturasCompraDTO> FacturasCompraInfoAll
        {
            get { return _dataRepository.GetAllFacturasCompra(); }
        }

        public FacturasCompraDTO GetFacturasCompraById(int idFacturasCompra)
        {
            return _dataRepository.GetFacturasCompraById(idFacturasCompra);
        }

        public int InsertFacturasCompra(FacturasCompraDTO New)
        {
            return _dataRepository.InsertFacturasCompra(New);
        }

        //por el front
        public async Task UpdateFacturasCompra(FacturasCompraDTO UpdItem)
        {
            await _dataRepository.UpdateFacturasCompra(UpdItem);
        }

        public void DeleteFacturasCompraById(int idFacturasCompra)
        {
            _dataRepository.DeleteFacturasCompraById(idFacturasCompra);
        }

        //paginado
        public PagedResult<FacturasCompraDTO> GetFacturasCompraPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetFacturasCompraPaginados(pagina, pageSize, filtro, estado);
        }

        //exportar
        public List<FacturasCompraDTO> ObtenerFacturaCompraFiltradas(string? filtro, string? estado)
        {
            return _dataRepository.ObtenerFacturaCompraFiltradas(filtro, estado);
        }


        //generarFactura atomaticamente
        public async Task<(FacturasCompraDTO factura, List<DetalleFacturaCompraDTO> detalles)> ObtenerFacturaConDetallesAsync(int idFactura)
        {
            return await _dataRepository.ObtenerFacturaConDetallesAsync(idFactura);
        }

        //busqueda facturas por número factura para activo que devuelve solo las facturas no ingresadas en factura
        public FacturasCompraDTO GetFacturasCompraByNumeroFactura(string numeroFactura)
        {
            return _dataRepository.GetFacturasCompraByNumeroFactura(numeroFactura);
        }

        //para compradirecta secuencial
        public string GenerarSecuencialFactura()
        {
            return _dataRepository.GenerarSecuencialFactura();
        }
    }
}
