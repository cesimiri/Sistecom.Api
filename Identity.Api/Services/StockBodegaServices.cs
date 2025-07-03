using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

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

        public void InsertStockBodega(stockBodegaDTO item)
        {
            _dataRepository.InsertStockBodega(item);
        }

        public void UpdateStockBodega(StockBodega item)
        {
            _dataRepository.UpdateStockBodega(item);
        }

        //public void DeleteStockBodega(StockBodega item)
        //{
        //    _dataRepository.DeleteStockBodega(item);
        //}

        public void DeleteStockBodegaById(int idStock)
        {
            _dataRepository.DeleteStockBodegaById(idStock);
        }

        //paginado
        public PagedResult<stockBodegaDTO> GetStockBodegaPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetStockBodegaPaginados(pagina, pageSize, filtro, estado);
        }
    }
}
