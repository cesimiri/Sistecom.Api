using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;


namespace Identity.Api.Interfaces
{
    public interface ICategoriasProducto
    {
        IEnumerable<CategoriasProducto> CategoriasProductoInfoAll { get; }
        CategoriasProducto GetCategoriasProductoById(int IdCategoriasProducto);
        void InsertCategoriasProducto(CategoriasProducto New);
        void UpdateCategoriasProducto(CategoriasProducto UpdItem);
        void DeleteCategoriasProducto(CategoriasProducto DelItem);
        void DeleteCategoriasProductoById(int IdCategoriasProducto);

        // Nuevo método para paginado:
        PagedResult<CategoriasProducto> GetCategoriasProductoPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
