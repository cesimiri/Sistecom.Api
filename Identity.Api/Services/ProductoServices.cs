using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class ProductoServices : IProducto
    {
        private ProductoRepository _dataRepository = new ProductoRepository();

        public IEnumerable<Producto> ProductoInfoAll
        {
            get { return _dataRepository.ProductoInfoAll(); }
        }

        public Producto GetProductoById(int IdProducto)
        {
            return _dataRepository.GetProductoById(IdProducto);
        }

        public void InsertProducto(Producto New)
        {
            _dataRepository.InsertProducto(New);
        }

        public void UpdateProducto(Producto UpdItem)
        {
            _dataRepository.UpdateProducto(UpdItem);
        }

        public void DeleteProducto(Producto DelItem)
        {
            _dataRepository.DeleteProducto(DelItem);
        }

        public void DeleteProductoById(int IdProducto)
        {
            _dataRepository.DeleteProductoById(IdProducto);
        }
    }
}
