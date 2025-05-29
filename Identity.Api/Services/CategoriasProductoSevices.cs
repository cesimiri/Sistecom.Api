using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class CategoriasProductoSevices : ICategoriasProducto
    {
        private CategoriasProductoRepository _dataRepository = new CategoriasProductoRepository();

        public IEnumerable<CategoriasProducto> CategoriasProductoInfoAll
        {
            get { return _dataRepository.CategoriasProductoInfoAll(); }
        }

        public CategoriasProducto GetCategoriasProductoById(int IdCategoriasProducto)
        {
            return _dataRepository.GetCategoriasProductoById(IdCategoriasProducto);
        }

        public void InsertCategoriasProducto(CategoriasProducto New)
        {
            _dataRepository.InsertCategoriasProducto(New);
        }

        public void UpdateCategoriasProducto(CategoriasProducto UpdItem)
        {
            _dataRepository.UpdateCategoriasProducto(UpdItem);
        }

        public void DeleteCategoriasProducto(CategoriasProducto DelItem)
        {
            _dataRepository.DeleteCategoriasProducto(DelItem);
        }

        public void DeleteCategoriasProductoById(int IdCategoriasProducto)
        {
            _dataRepository.DeleteCategoriasProductoById(IdCategoriasProducto);
        }
    }
}
