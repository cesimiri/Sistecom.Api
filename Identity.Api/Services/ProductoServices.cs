using Identity.Api.Interfaces;
using Identity.Api.DataRepository;

using Identity.Api.DTO;

namespace Identity.Api.Services
{
    public class ProductoServices : IProducto
    {
        private ProductoRepository _dataRepository = new ProductoRepository();

        public IEnumerable<ProductoDTO> GetAllProducto
        {
            get { return _dataRepository.GetAllProducto(); }
        }

        public ProductoDTO GetProductoById(int idProducto)
        {
            return _dataRepository.GetProductoById(idProducto);
        }

        public void InsertProducto(ProductoDTO New)
        {
            _dataRepository.InsertProducto(New);
        }

        public void UpdateProducto(ProductoDTO UpdItem)
        {
            _dataRepository.UpdateProducto(UpdItem);
        }


        public void DeleteProductoById(int idProducto)
        {
            _dataRepository.DeleteProductoById(idProducto);
        }

        // referenciables por espera de otra seleccion
        public IEnumerable<ModeloDTO> GetModelosByIdMarca(int idMarca)
        {
            return _dataRepository.GetModelosByIdMarca(idMarca);
        }
    }
}
