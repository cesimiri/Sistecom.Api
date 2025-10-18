using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IServidore
    {
        IEnumerable<Servidore> ServidoreInfoAll { get; }
        //Para trawer todos los activos donde sea el nombre del producto servidores o servidor en mayuscula 
        IEnumerable<ActivoDTO> GetActivosServidores { get; }
        Servidore GetServidoreById(int IdServidore);
        void InsertServidore(ServidoreDTO New);
        void UpdateServidore(Servidore UpdItem);
        void DeleteServidoreById(int IdServidore);
        PagedResult<ServidoreDTO> GetServidorePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);

    }
}
