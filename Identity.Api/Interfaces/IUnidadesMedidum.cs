using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IUnidadesMedidum
    {
        IEnumerable<UnidadesMedidum> GetAllUnidades();
        UnidadesMedidum GetUnidadesById(int idUnidades);
        void InsertUnidades(UnidadesMedidumDTO dto);
        void UpdateUnidades(UnidadesMedidum dto);
        //void DeleteStockBodega(StockBodega item);
        void DeleteUnidadesById(int idUnidades);
    }
}
