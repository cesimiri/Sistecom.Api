using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
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

        public void InsertStockBodega(StockBodega item)
        {
            _dataRepository.InsertStockBodega(item);
        }

        public void UpdateStockBodega(StockBodega item)
        {
            _dataRepository.UpdateStockBodega(item);
        }

        public void DeleteStockBodega(StockBodega item)
        {
            _dataRepository.DeleteStockBodega(item);
        }

        public void DeleteStockBodegaById(int idStock)
        {
            _dataRepository.DeleteStockBodegaById(idStock);
        }
    }
}
