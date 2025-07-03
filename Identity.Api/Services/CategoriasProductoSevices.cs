using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
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


        //paginado
        public PagedResult<CategoriasProducto> GetCategoriasProductoPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetCategoriasProductoPaginados(pagina, pageSize, filtro, estado);
        }
    }
}
