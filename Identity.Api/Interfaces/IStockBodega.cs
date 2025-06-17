using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface IStockBodega
    {
        IEnumerable<StockBodega> GetAllStockBodega();
        StockBodega GetStockBodegaById(int idStock);
        void InsertStockBodega(stockBodegaDTO item);
        void UpdateStockBodega(StockBodega item);
        //void DeleteStockBodega(StockBodega item);
        void DeleteStockBodegaById(int idStock);
    }
}
