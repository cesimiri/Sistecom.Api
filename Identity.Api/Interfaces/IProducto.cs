using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IProducto
    {
        IEnumerable<Producto> ProductoInfoAll { get; }
        Producto GetProductoById(int IdProducto);
        void InsertProducto(Producto New);
        void UpdateProducto(Producto UpdItem);
        void DeleteProducto(Producto DelItem);
        void DeleteProductoById(int IdProducto);
    }
}
