using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class StockBodegaServices : IStockBodega
    {
        private readonly StockBodegaDataRepository _dataRepository = new StockBodegaDataRepository();

        public IEnumerable<StockBodega> GetAllStockBodega()
        {
            return _dataRepository.GetAllStockBodega();
        }

        public StockBodega GetStockBodegaById(int idStock)
        {
            return _dataRepository.GetStockBodegaById(idStock);
        }



        public void UpdateStockBodega(StockBodega item)
        {
            _dataRepository.UpdateStockBodega(item);
        }

        public void DeleteStockBodegaById(int idStock)
        {
            _dataRepository.DeleteStockBodegaById(idStock);
        }


        //paginado por bodega
        public PagedResult<stockBodegaDTO> GetPaginadosPorBodega(int idBodega, int pagina, int pageSize, string? filtro = null)
        {
            return _dataRepository.GetPaginadosPorBodega(idBodega, pagina, pageSize, filtro);
        }


        //exportar
        public List<stockBodegaDTO> ObtenerParaExportar(int idBodega, string? filtro = null, string? correo = null)
        {
            return _dataRepository.ObtenerParaExportar(idBodega, filtro, correo);
        }

    }
}
