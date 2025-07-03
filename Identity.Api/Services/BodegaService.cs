using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class BodegaService : IBodega
    {
        private BodegaRepository _dataRepository = new BodegaRepository();

        public IEnumerable<Bodega> BodegaInfoAll
        {
            get { return _dataRepository.GetAllBodega(); }
        }

        public Bodega GetBodegaById(int IdBodega)
        {
            return _dataRepository.GetBodegaById(IdBodega);
        }

        public void InsertBodega(BodegaDTO New)
        {
            _dataRepository.InsertBodega(New);
        }

        public void UpdateBodega(Bodega UpdItem)
        {
            _dataRepository.UpdateBodega(UpdItem);
        }

        public void DeleteBodega(Bodega DelItem)
        {
            _dataRepository.DeleteBodega(DelItem);
        }

        public void DeleteBodegaById(int IdBodega)
        {
            _dataRepository.DeleteBodegaById(IdBodega);
        }

        //paginado
        public PagedResult<BodegaDTO> GetBodegaPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetBodegaPaginados(pagina, pageSize, filtro, estado);
        }
    }
}
