using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Services
{
    public class SolicitudesCompraService : ISolicitudesCompra
    {
        private readonly SolicitudesCompraDataRepository _dataRepository = new SolicitudesCompraDataRepository();

        public IEnumerable<SolicitudesCompra> SolicitudesCompraAll
        {
            get { return _dataRepository.GetAllSolicitudesCompra(); }
        }

        public SolicitudesCompra GetSolicitudById(int idSolicitud)
        {
            return _dataRepository.GetSolicitudById(idSolicitud);
        }

        public void InsertSolicitud(SolicitudesCompra newSolicitud)
        {
            _dataRepository.InsertSolicitud(newSolicitud);
        }

        public void UpdateSolicitud(SolicitudesCompra updatedSolicitud)
        {
            _dataRepository.UpdateSolicitud(updatedSolicitud);
        }

        public void DeleteSolicitud(SolicitudesCompra solicitudToDelete)
        {
            _dataRepository.DeleteSolicitud(solicitudToDelete);
        }

        public void DeleteSolicitudById(int idSolicitud)
        {
            _dataRepository.DeleteSolicitudById(idSolicitud);
        }
    }
}
