using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IMarca
    {
        IEnumerable<Marca> GetAllMarca { get; }
        Marca GetMarcaById(int idMarca);
        void InsertMarca(MarcaDTO item);
        void UpdateMarca(Marca item);
        //void DeleteStockBodega(StockBodega item);
        void DeleteMarcaById(int idMarca);
    }
}
