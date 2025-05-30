using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class DetalleEnsamblajeServices : IDetalleEnsamblaje
    {

        private DetalleEnsamblajeRepository _dataRepository = new DetalleEnsamblajeRepository();

        public IEnumerable<DetalleEnsamblaje> DetalleEnsamblajeInfoAll
        {
            get { return _dataRepository.DetalleEnsamblajeInfoAll(); }
        }

        public DetalleEnsamblaje GetDetalleEnsamblajeById(int idDetalleEnsamblaje)
        {
            return _dataRepository.GetDetalleEnsamblajeById(idDetalleEnsamblaje);
        }

        public void InsertDetalleEnsamblaje(DetalleEnsamblaje New)
        {
            _dataRepository.InsertDetalleEnsamblaje(New);
        }

        public void UpdateDetalleEnsamblaje(DetalleEnsamblaje UpdItem)
        {
            _dataRepository.UpdateDetalleEnsamblaje(UpdItem);
        }

        public void DeleteDetalleEnsamblaje(DetalleEnsamblaje DelItem)
        {
            _dataRepository.DeleteDetalleEnsamblaje(DelItem);
        }

        public void DeleteDetalleEnsamblajeById(int idDetalleEnsamblaje)
        {
            _dataRepository.DeleteDetalleEnsamblajeById(idDetalleEnsamblaje);
        }
    }
}
