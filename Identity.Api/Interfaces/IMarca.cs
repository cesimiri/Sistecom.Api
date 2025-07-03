using Identity.Api.DTO;
using Identity.Api.Paginado;
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

        // Nuevo método para paginado:
        PagedResult<MarcaDTO> GetMarcaPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
