using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class DetalleOrdenEntregaServices : IDetalleOrdenEntrega
    {
        private DetalleOrdenEntregaRepository _dataRepository = new DetalleOrdenEntregaRepository();

        public IEnumerable<DetalleOrdenEntrega> DetalleOrdenEntregaInfoAll
        {
            get { return _dataRepository.DetalleOrdenEntregaInfoAll(); }
        }

        public DetalleOrdenEntrega GetDetalleOrdenEntregaById(int idDetalleOrdenEntrega)
        {
            return _dataRepository.GetDetalleOrdenEntregaById(idDetalleOrdenEntrega);
        }

        public void InsertDetalleOrdenEntrega(DetalleOrdenEntrega New)
        {
            _dataRepository.InsertDetalleOrdenEntrega(New);
        }

        public void UpdateDetalleOrdenEntrega(DetalleOrdenEntrega UpdItem)
        {
            _dataRepository.UpdateDetalleOrdenEntrega(UpdItem);
        }

        public void DeleteDetalleOrdenEntrega(DetalleOrdenEntrega DelItem)
        {
            _dataRepository.DeleteDetalleOrdenEntrega(DelItem);
        }

        public void DeleteDetalleOrdenEntregaById(int idDetalleOrdenEntrega)
        {
            _dataRepository.DeleteDetalleOrdenEntregaById(idDetalleOrdenEntrega);
        }
    }
}

