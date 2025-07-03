using Identity.Api.DTO;
using Identity.Api.Paginado;
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


        // Nuevo método para paginado:
        PagedResult<stockBodegaDTO> GetStockBodegaPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
