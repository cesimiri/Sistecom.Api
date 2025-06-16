using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Interfaces;
using Identity.Api.DTO;

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
    }
}
