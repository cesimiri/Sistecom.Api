using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IProducto
    {
        IEnumerable<ProductoDTO> GetAllProducto { get; }
        ProductoDTO GetProductoById(int idProducto);
        void InsertProducto(ProductoDTO dto);
        void UpdateProducto(ProductoDTO dto);
        //void DeleteUsuario(UsuarioDTO dto);
        void DeleteProductoById(int idProducto);

        IEnumerable<ModeloDTO> GetModelosByIdMarca(int idMarca);

        // Nuevo método para paginado:
        PagedResult<ProductoDTO> GetProductoPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
