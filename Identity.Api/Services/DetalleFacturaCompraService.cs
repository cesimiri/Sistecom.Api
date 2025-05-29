using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class DetalleFacturaCompraService : IDetalleFacturaCompra
    {
        private readonly DetalleFacturaCompraDataRepository _dataRepository = new DetalleFacturaCompraDataRepository();

        public IEnumerable<DetalleFacturaCompra> DetallesFacturaCompraInfoAll
        {
            get { return _dataRepository.GetAllDetallesFacturaCompra(); }
        }

        public DetalleFacturaCompra GetDetalleFacturaCompraById(int idDetalle)
        {
            return _dataRepository.GetDetalleFacturaCompraById(idDetalle);
        }

        public void InsertDetalleFacturaCompra(DetalleFacturaCompra NewItem)
        {
            _dataRepository.InsertDetalleFacturaCompra(NewItem);
        }

        public void UpdateDetalleFacturaCompra(DetalleFacturaCompra UpdItem)
        {
            _dataRepository.UpdateDetalleFacturaCompra(UpdItem);
        }

        public void DeleteDetalleFacturaCompra(DetalleFacturaCompra DelItem)
        {
            _dataRepository.DeleteDetalleFacturaCompra(DelItem);
        }

        public void DeleteDetalleFacturaCompraById(int idRegistrado)
        {
            _dataRepository.DeleteDetalleFacturaCompraById(idRegistrado);
        }
    }
}
