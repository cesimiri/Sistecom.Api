using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IActivo
    {
        IEnumerable<Activo> ActivoInfoAll { get; }
        Activo GetActivoById(int IdActivo);
        void InsertActivo(Activo New);
        void UpdateActivo(Activo UpdItem);
        void DeleteActivo(Activo DelItem);
        void DeleteActivoById(int IdActivo);

        //PAGINADO
        PagedResult<ActivoDTO> GetPaginados(
        int pagina,
        int pageSize,
        string? codigoActivo,
        int? idProducto,
        DateTime? desde,
        DateTime? hasta,
        int? idFacturaCompra,
        string? estadoActivo,
        string? ordenColumna = null,
        bool ordenAscendente = true
        );

    }
}
