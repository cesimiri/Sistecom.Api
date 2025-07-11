using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

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





        //paginado
        PagedResult<stockBodegaDTO> GetPaginadosPorBodega(int idBodega, int pagina, int pageSize, string? filtro = null);


        //actualiza el stock de la bodega 
        bool ProcesarMovimientoStock(List<MovimientosInventarioDTO> movimientos, out string error);
    }
}
