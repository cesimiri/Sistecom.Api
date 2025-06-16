using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using Identity.Api.DTO;

namespace Identity.Api.Services
{
    public class DetalleSolicitudService : IDetalleSolicitud
    {
        private DetalleSolicitudDataRepository _dataRepository = new DetalleSolicitudDataRepository();

        public IEnumerable<DetalleSolicitudDTO> DetalleSolicitudesAll
        {
            get { return _dataRepository.DetalleSolicitudesAll(); }
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
    }
}
