using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;

namespace Identity.Api.Services
{
    public class DetalleSolicitudService : IDetalleSolicitud
    {
        private DetalleSolicitudDataRepository _dataRepository = new DetalleSolicitudDataRepository();

        public IEnumerable<DetalleSolicitudDTO> DetalleSolicitudesAll
        {
            get { return _dataRepository.DetalleSolicitudesAll(); }
        }

        //para traer todos lso registro por la solicitud de compra 
        public List<DetalleSolicitudDTO> GetDetalleSolicitudByIdSolicitud(int idSolicitud)
        {
            return _dataRepository.GetDetalleSolicitudByIdSolicitud(idSolicitud);
        }

        public DetalleSolicitudDTO GetDetalleSolicitudById(int idDetalle)
        {
            return _dataRepository.GetDetalleSolicitudById(idDetalle);
        }
        public void InsertDetalleSolicitud(DetalleSolicitudDTO newItem)
        {
            _dataRepository.InsertDetalleSolicitud(newItem);
        }

        public void UpdateDetalleSolicitud(DetalleSolicitudDTO updItem)
        {
            _dataRepository.UpdateDetalleSolicitud(updItem);
        }

        //public void DeleteDetalleSolicitud(DetalleSolicitudDTO delItem)
        //{
        //    _dataRepository.DeleteDetalleSolicitud(delItem);
        //}

        public void DeleteDetalleSolicitudById(int idDetalle)
        {
            _dataRepository.DeleteDetalleSolicitudById(idDetalle);
        }

        public void InsertarDetallesMasivos(List<DetalleSolicitudDTO> lista)
        {
            _dataRepository.InsertarDetallesMasivos(lista);
        }

        public IEnumerable<SolicitudesCompraDTO> SolicitudesDeCompraPorEstadoAsync()
        {
            return _dataRepository.SolicitudesDeCompraPorEstadoAsync();
        }

        public IEnumerable<DetalleSolicitudDTO> GetDetallesBySolicitudId(int idSolicitud)
        {
            return _dataRepository.GetDetallesBySolicitudId(idSolicitud);
        }

    }
}
