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
    }
}
