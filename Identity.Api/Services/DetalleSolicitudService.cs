using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Services
{
    public class DetalleSolicitudService : IDetalleSolicitud
    {
        private DetalleSolicitudDataRepository _dataRepository = new DetalleSolicitudDataRepository();

        public IEnumerable<DetalleSolicitud> DetalleSolicitudesInfoAll
        {
            get { return _dataRepository.GetAllDetalleSolicitudes(); }
        }

        public DetalleSolicitud GetDetalleSolicitudById(int idDetalle)
        {
            return _dataRepository.GetDetalleSolicitudById(idDetalle);
        }

        public void InsertDetalleSolicitud(DetalleSolicitud newItem)
        {
            _dataRepository.InsertDetalleSolicitud(newItem);
        }

        public void UpdateDetalleSolicitud(DetalleSolicitud updItem)
        {
            _dataRepository.UpdateDetalleSolicitud(updItem);
        }

        public void DeleteDetalleSolicitud(DetalleSolicitud delItem)
        {
            _dataRepository.DeleteDetalleSolicitud(delItem);
        }

        public void DeleteDetalleSolicitudById(int idDetalle)
        {
            _dataRepository.DeleteDetalleSolicitudById(idDetalle);
        }
    }
}
