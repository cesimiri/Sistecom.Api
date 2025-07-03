using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.Services
{

    public class FacturasCompraServices : IFacturasCompra
    {
        private FacturasCompraRepository _dataRepository = new FacturasCompraRepository();

        public IEnumerable<FacturasCompraDTO> FacturasCompraInfoAll
        {
            get { return _dataRepository.GetAllFacturasCompra(); }
        }

        public FacturasCompra GetFacturasCompraById(int idFacturasCompra)
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

        //public void DeleteFacturasCompra(FacturasCompra DelItem)
        //{
        //    _dataRepository.DeleteFacturasCompra(DelItem);
        //}

        public void DeleteFacturasCompraById(int idFacturasCompra)
        {
            _dataRepository.DeleteFacturasCompraById(idFacturasCompra);
        }

        //paginado
        public PagedResult<FacturasCompraDTO> GetFacturasCompraPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetFacturasCompraPaginados(pagina, pageSize, filtro, estado);
        }
    }
}
